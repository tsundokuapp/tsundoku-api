using System.Configuration;

namespace TsundokuTraducoes.Helpers.Configuration
{
    public static class ConfigurationExternal
    {
        private static AcessoExternoTinify _acessoExternoTinify;
        private static AcessoExternoAws _accesoExternoAws;

        public static void SetaAcessoExterno(AcessoExternoTinify acessoExternoTinify, AcessoExternoAws acessoExternoAws)
        {
            _acessoExternoTinify = acessoExternoTinify;
            _accesoExternoAws = acessoExternoAws;
        }

        public static string RetornaApiKeyTinify()
        {   
            var apiKeyTinify = ConfigurationManager.AppSettings["ApiKeyTinify"];
            apiKeyTinify ??= _acessoExternoTinify.ApiKeyTinify;

            return apiKeyTinify;
        }

        public static string RetornaAwsAccessKeyId()
        {
            var awsAccessKeyId = ConfigurationManager.AppSettings["AwsAccessKeyId"];
            awsAccessKeyId ??= _accesoExternoAws.AwsAccessKeyId;

            return awsAccessKeyId;
        }

        public static string RetornaAwsSecretAccessKey()
        {            
            var awsSecretAccessKey = ConfigurationManager.AppSettings["AwsSecretAccessKey"];
            awsSecretAccessKey ??= _accesoExternoAws.AwsSecretAccessKey;

            return awsSecretAccessKey;
        }

        public static string RetornaBucketName()
        {             
            var bucketName = ConfigurationManager.AppSettings["BucketName"];
            bucketName ??= _accesoExternoAws.BucketName;

            return bucketName;
        }

        public static string RetornaDistributionId()
        {            
            var distributionId = ConfigurationManager.AppSettings["DistributionId"];
            distributionId ??= _accesoExternoAws.DistributionId;

            return distributionId;
        }

        public static string RetornaDistributionDomainName()
        {            
            var distributionDomainName = ConfigurationManager.AppSettings["DistributionDomainName"];
            distributionDomainName ??= _accesoExternoAws.DistributionDomainName;

            return distributionDomainName;
        }
    }

    public class AcessoExternoTinify
    {
        public string ApiKeyTinify { get; set; }
    }

    public class AcessoExternoAws 
    {
        public string AwsAccessKeyId { get; set; }
        public string AwsSecretAccessKey { get; set; }
        public string BucketName { get; set; }
        public string DistributionId { get; set; }
        public string DistributionDomainName { get; set; }
    }
}
