using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CopyImagesToCopies
{
    public class CopyImages
    {
        [FunctionName("CopyImages")]
        public async Task Run(
            [BlobTrigger("images/{name}", Connection = "BlobConnectionString")]Stream myBlob,
            [Blob("copies/{name}", FileAccess.Write, Connection = "BlobConnectionString")] Stream outputBlob, 
            string name, 
            ILogger log)
        {
            try
            {
                await myBlob.CopyToAsync(outputBlob);
                log.LogInformation($"{name} image is successfully copied from IMAGES container to COPIES container");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception : {ex.Message}");
            }
            finally
            {
                myBlob.Close();
                outputBlob.Close();
            }
        }
    }
}
