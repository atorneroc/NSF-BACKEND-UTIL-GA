using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Npgsql;
using Scharff.API.Utils.PipelineBehaviour;
using Scharff.API.Utils.Utils.GlobalHandlers;
using Scharff.Infrastructure.AzureBlobStorage.Queries.DownloadFile;
using Scharff.Infrastructure.AzureBlobStorage.Queries.GetAllFiles;
using Scharff.Infrastructure.AzureBlobStorage.Repositories.DeleteFile;
using Scharff.Infrastructure.AzureBlobStorage.Repositories.UploadFile;
using Scharff.Infrastructure.Http.Queries.PaymentVoucher.GetPaymentVoucherByExchangeRate;
using Scharff.Infrastructure.Http.Queries.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRange;
using Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.RegisterExchangeRate;
using Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.UpdateExchangeRate;
using Scharff.Infrastructure.PostgreSQL.Queries.BillingCourt.GetBusinessUnitByIdCompany;
using Scharff.Infrastructure.PostgreSQL.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany;
using Scharff.Infrastructure.PostgreSQL.Queries.Client.VerifyIdentityClient;
using Scharff.Infrastructure.PostgreSQL.Queries.Company.GetSunatRegimenById;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetAllExchangeRate;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateBroadCast;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateById;
using Scharff.Infrastructure.PostgreSQL.Queries.LedgerAccount.GetAllLedgerAccount;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.CheckExternalExistenceUser;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.Generic;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetAllCompanies;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetAssignmentCustomerByDocumentId;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetBranchOfficeByCompanyId;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetBusinessUnitByBranchOfficeId;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetIgv;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetIntegrationCodeByIdTypeVoucher;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetParameterByDetailCode;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetParameterByGroupId;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetStoreByBusinessUnitId;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateConfiguredService;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateConfiguredServiceFree;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateExternalBaseStructure;
using Scharff.Infrastructure.PostgreSQL.Queries.PaymentVoucherConfiguration.GetPaymentVoucherConfiguration;
using Scharff.Infrastructure.PostgreSQL.Queries.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;
using Scharff.Infrastructure.PostgreSQL.Queries.Reports.GetConsolidatedReport;
using Scharff.Infrastructure.PostgreSQL.Queries.Reports.GetReportServiceOrderCourier;
using Scharff.Infrastructure.PostgreSQL.Queries.Reports.Report;
using Scharff.Infrastructure.PostgreSQL.Queries.Security.GetAccessByUser;
using Scharff.Infrastructure.PostgreSQL.Queries.Security.GetVerifyMenuRoutByUserRout;
using Scharff.Infrastructure.PostgreSQL.Queries.Service.GetAllService;
using Scharff.Infrastructure.PostgreSQL.Queries.UbicacionGeografica.GetUbicacionGeograficaById;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.CheckUbigeoByCode;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.CheckUbigeoBySapCodes;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetAllCountryRegion;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetByIdCountryRegion;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCodeQuery;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCountryQuery;
using Scharff.Infrastructure.PostgreSQL.Queries.User.GetUserByProfileId;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("secrets/appsettings.secrets.json", optional: true);
builder.Services.AddTransient<IDbConnection>(x => new NpgsqlConnection(builder.Configuration.GetConnectionString("DB_CONN_STR")));

var originsValue = builder.Configuration.GetValue<string>("Origins");
string[] origins = (originsValue ?? string.Empty)
    .Split(",", StringSplitOptions.RemoveEmptyEntries);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCors",
                      builderCors =>
                      {
                          builderCors.WithOrigins(origins)
                                     .AllowAnyHeader()
                                     .AllowAnyMethod()
                                     .WithExposedHeaders("Content-Disposition"); 
                      });
});

builder.Services.AddHttpClient<GetPaymentVoucherByExchangeRateQuery>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("ApiBillingConnectionString") ?? "");
});

builder.Services.AddTransient(typeof(IGetParameterByGroupIdQuery), typeof(GetParameterByGroupIdQuery));
builder.Services.AddTransient(typeof(IGetAllCompaniesQuery), typeof(GetAllCompaniesQuery));
builder.Services.AddTransient(typeof(IGetBranchOfficeByCompanyIdQuery), typeof(GetBranchOfficeByCompanyIdQuery));
builder.Services.AddTransient(typeof(IGetBusinessUnitByBranchOfficeIdQuery), typeof(GetBusinessUnitByBranchOfficeIdQuery));
builder.Services.AddTransient(typeof(IGetParameterByDetailCodeQuery), typeof(GetParameterByDetailCodeQuery));

builder.Services.AddTransient(typeof(IGetExchangeRateBroadCast), typeof(GetExchangeRateBroadCast));
builder.Services.AddTransient(typeof(IVerifyIdentityClientQuery), typeof(VerifyIdentityClientQuery));
builder.Services.AddTransient(typeof(IGetStoreByBusinessUnitIdQuery), typeof(GetStoreByBusinessUnitIdQuery));
builder.Services.AddTransient(typeof(IGetIgvQuery), typeof(GetIgvQuery));

builder.Services.AddTransient(typeof(IGetAllFilesQuery), typeof(GetAllFilesQuery));
builder.Services.AddTransient(typeof(IDownloadFileQuery), typeof(DownloadFileQuery));
builder.Services.AddTransient(typeof(IUploadFileRepository), typeof(UploadFileRepository));
builder.Services.AddTransient(typeof(IDeleteFileRepository), typeof(DeleteFileRepository));
builder.Services.AddTransient(typeof(IGetReportQuery), typeof(GetReportQuery));

builder.Services.AddTransient(typeof(IGetAllCountryRegionQuery), typeof(GetAllCountryRegionQuery));
builder.Services.AddTransient(typeof(IGetUbigeoByCodeQuery), typeof(Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCodeQuery.GetUbigeoByCodeQuery));
builder.Services.AddTransient(typeof(ICheckUbigeoByCodeQuery), typeof(CheckUbigeoByCodeQuery));

builder.Services.AddTransient(typeof(IGetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery), typeof(GetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery));
builder.Services.AddTransient(typeof(IGetUbicacionGeograficaByIdQuery), typeof(GetUbicacionGeograficaByIdQuery));

builder.Services.AddTransient(typeof(IGetAllServiceQuery), typeof(GetAllServiceQuery));
builder.Services.AddTransient(typeof(IGetAllLedgerAccountQuery), typeof(GetAllLedgerAccountQuery));

builder.Services.AddTransient(typeof(IGetAccessByUserQuery), typeof(GetAccessByUserQuery));
builder.Services.AddTransient(typeof(IGetVerifyMenuRoutByUserRoutQuery), typeof(GetVerifyMenuRoutByUserRoutQuery));
builder.Services.AddTransient(typeof(IGetPaymentVoucherConfigurationQuery), typeof(GetPaymentVoucherConfigurationQuery));

builder.Services.AddTransient(typeof(IGetSunatRegimenByIdQuery), typeof(GetSunatRegimenByIdQuery));
builder.Services.AddTransient(typeof(IGetUserByProfileIdQuery), typeof(GetUserByProfileIdQuery));

builder.Services.AddTransient(typeof(IGetBusinessUnitByIdCompanyQuery), typeof(GetBusinessUnitByIdCompanyQuery));
builder.Services.AddTransient(typeof(IGetProductByIdBusinessUnitIdCompanyQuery), typeof(GetProductByIdBusinessUnitIdCompanyQuery));
builder.Services.AddTransient(typeof(IGetAllExchangeRateQuery), typeof(GetAllExchangeRateQuery));
builder.Services.AddTransient(typeof(IRegisterExchangeRateCommand), typeof(RegisterExchangeRateCommand));
builder.Services.AddTransient(typeof(IUpdateExchangeRateCommand), typeof(UpdateExchangeRateCommand));
builder.Services.AddTransient(typeof(IGetExchangeRateByIdQuery), typeof(GetExchangeRateByIdQuery));
builder.Services.AddTransient(typeof(IGetPaymentVoucherByExchangeRateQuery), typeof(GetPaymentVoucherByExchangeRateQuery));
builder.Services.AddTransient(typeof(IGetReportInvoiceServiceByIdTypeServicesAndDateRange), typeof(GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery));
builder.Services.AddTransient(typeof(IGetConsolidatedReport), typeof(GetConsolidatedReportQuery));
builder.Services.AddTransient(typeof(IGetReportServiceOrderCourierQuery), typeof(GetReportServiceOrderCourierQuery));

builder.Services.AddTransient(typeof(IUploadFileV2Repository), typeof(UploadFileV2Repository));
builder.Services.AddTransient(typeof(IGetAllFilesV2Query), typeof(GetAllFilesV2Query));
builder.Services.AddTransient(typeof(IDownloadFileV2Query), typeof(DownloadFileV2Query));

builder.Services.AddTransient(typeof(IValidateExternalBaseStructureQuery), typeof(Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateExternalBaseStructure.ValidateExternalBaseStructureQuery));


builder.Services.AddTransient(typeof(IGenericQuery), typeof(GenericQuery));
builder.Services.AddTransient(typeof(ICheckExternalExistenceUserQuery), typeof(Scharff.Infrastructure.PostgreSQL.Queries.Parameter.CheckExternalExistenceUser.CheckExternalExistenceUserQuery));

builder.Services.AddTransient(typeof(IGetByIdCountryRegionQuery), typeof(GetByIdCountryRegionQuery));
builder.Services.AddTransient(typeof(IGetUbigeoByCountryQuery), typeof(Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCountryQuery.GetUbigeoByCountryQuery));
builder.Services.AddTransient(typeof(IValidateConfiguredServiceQuery), typeof(ValidateConfiguredServiceQuery));
builder.Services.AddTransient(typeof(IValidateConfiguredServiceFreeQuery), typeof(ValidateConfiguredServiceFreeQuery));
builder.Services.AddTransient(typeof(IGetIntegrationCodeByIdTypeVoucherQuery), typeof(GetIntegrationCodeByIdTypeVoucherQuery));
builder.Services.AddTransient(typeof(ICheckUbigeoBySapCodesQuery), typeof(CheckUbigeoBySapCodesQuery));
builder.Services.AddTransient(typeof(IGetAssignmentCustomerByDocumentIdQuery), typeof(GetAssignmentCustomerByDocumentIdQuery));

Assembly application = AppDomain.CurrentDomain.Load("Scharff.Application");
builder.Services.AddMediatR(application);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssembly(application);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("MyCors");
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<GlobalErrorHandler>();

app.Run();