using Amazon;
using Amazon.CloudFront;
using Amazon.CloudFront.Model;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using TsundokuTraducoes.Helpers.Configuration;

namespace TsundokuTraducoes.Helpers.Imagens
{
    public class ServicosImagemAmazonS3
    {
        private static IAmazonS3 _s3Client;
        
        private static IAmazonCloudFront _cloudFrontClient;
                
        private string _awsAccessKeyId => ConfigurationExternal.RetornaAwsAccessKeyId();
        private string _awsSecretAccessKey => ConfigurationExternal.RetornaAwsSecretAccessKey();
        private string _bucketName => ConfigurationExternal.RetornaBucketName();
        private string _distributionId => ConfigurationExternal.RetornaDistributionId();
        public string _distributionDomainName => ConfigurationExternal.RetornaDistributionDomainName();

        public RegionEndpoint _regionEndpoint => RegionEndpoint.USEast1;
        public AWSCredentials _AWSCredentials;

        public ServicosImagemAmazonS3()
        {
            _AWSCredentials = new BasicAWSCredentials(_awsAccessKeyId, _awsSecretAccessKey);
            _s3Client = new AmazonS3Client(_AWSCredentials, _regionEndpoint);
            _cloudFrontClient = new AmazonCloudFrontClient(_AWSCredentials, _regionEndpoint);
        }

        public async Task<bool> CriarPastaS3(string nomePasta)
        {
            var retorno = true;
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = nomePasta
            };

            try
            {
                var retornoPutObjectAsync = await _s3Client.PutObjectAsync(request);
                if (retornoPutObjectAsync.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    retorno = false;
            }
            catch (Exception)
            {
                retorno = false;
            }

            return retorno;
        }

        public async Task<bool> VerificaObjetoExistenteAwsS3(string nomePasta)
        {
            var retorno = true;
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = nomePasta
            };

            try
            {
                var retornoObjectAsync = await _s3Client.GetObjectAsync(request);
                if (retornoObjectAsync.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    return false;
            }
            catch (Exception)
            {
                retorno = false;
            }

            return retorno;
        }

        public async Task<bool> UploadImagem(Stream stream, string caminhoCompletoImagem, bool alterarImagem)
        {
            var retorno = true;

            var request = new PutObjectRequest()
            {
                InputStream = stream,
                BucketName = _bucketName,
                Key = caminhoCompletoImagem,
            };

            try
            {
                var retornoPutObjectAsync = await _s3Client.PutObjectAsync(request);
                if (retornoPutObjectAsync.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    retorno = false;
            }
            catch (Exception)
            {
                retorno = false;
            }            

            if (alterarImagem)
            {
                if (!await AtualizaCache(caminhoCompletoImagem))
                    retorno = false;
            }

            return retorno;
        }

        public async Task<bool> AtualizaCache(string caminhoCompletoImagem)
        {
            var retorno = true;
            string imageToInvalidate = $"/{caminhoCompletoImagem}";

            var createInvalidationRequest = new CreateInvalidationRequest
            {
                DistributionId = _distributionId,
                InvalidationBatch = new InvalidationBatch
                {
                    Paths = new Paths
                    {
                        Quantity = 1,
                        Items = new List<string> { imageToInvalidate }
                    },

                    CallerReference = DateTime.Now.Ticks.ToString()
                }
            };

            try
            {
                var result = await _cloudFrontClient.CreateInvalidationAsync(createInvalidationRequest);
                if (result.HttpStatusCode != System.Net.HttpStatusCode.Created)
                    retorno = false;
            }
            catch (Exception)
            {
                retorno = false;
            }

            return retorno;
        }

        public async Task<bool> ExcluiObjetoBucket(string keyName)
        {
            var retorno = true;

            try
            {
                var pastaExistente = await VerificaObjetoExistenteAwsS3(keyName);
                if (pastaExistente)
                {
                    var request = new ListObjectsRequest
                    {
                        BucketName = _bucketName,
                        Prefix = keyName,
                    };

                    var response = await _s3Client.ListObjectsAsync(request);
                    var keys = new List<KeyVersion>();
                    var keysImagem = new List<KeyVersion>();
                    foreach (var item in response.S3Objects)
                    {
                        keys.Add(new KeyVersion { Key = item.Key });

                        if (item.Key != keyName)
                            keysImagem.Add(new KeyVersion { Key = item.Key });
                    }

                    var multiObjectDeleteRequest = new DeleteObjectsRequest()
                    {
                        BucketName = _bucketName,
                        Objects = keys
                    };

                    await _s3Client.DeleteObjectsAsync(multiObjectDeleteRequest);

                    foreach (var item in keysImagem)
                    {
                        await AtualizaCache(item.Key);
                    }
                }
                else
                {
                    retorno = false;
                }
            }
            catch (AmazonS3Exception)
            {
                retorno = false;
            }

            return retorno;
        }
    }
}
