using Microsoft.Extensions.Configuration;
using Scharff.Domain.Response.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Scharff.Infrastructure.Http.Queries.PaymentVoucher.GetPaymentVoucherByExchangeRate
{
    public class GetPaymentVoucherByExchangeRateQuery : IGetPaymentVoucherByExchangeRateQuery
    {
        private readonly string _apiBillingConnectionString;
        private readonly HttpClient _client;
        public GetPaymentVoucherByExchangeRateQuery(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _apiBillingConnectionString = configuration.GetConnectionString("ApiBillingConnectionString") ?? "";
        }
        public async Task<int> GetPaymentVoucherByExchangeRate(string issue_date)
        {
            try
            {
                string apiUrl = $"{_apiBillingConnectionString}paymentvoucher/{issue_date}/exchangerate";

                HttpResponseMessage responseHttp = await _client.GetAsync(apiUrl);

                if (responseHttp.IsSuccessStatusCode)
                {
                    var responseBody = await responseHttp.Content.ReadAsStringAsync();
                    var customResponse = JsonSerializer.Deserialize<CustomResponseHttp<int>>(responseBody);
                    return customResponse?.data ?? 0;
                }
                else
                {
                    throw new HttpRequestException("");
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }
    }
}
