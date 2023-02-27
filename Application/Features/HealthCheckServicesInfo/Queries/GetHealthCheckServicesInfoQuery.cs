using healthcheck.api.Domain.Dto;
using MediatR;

namespace healthcheck.api.Application.Features.HealthCheckServicesInfo.Queries
{
    public class GetHealthCheckServicesInfoQuery : IRequest<IEnumerable<ServicesInfoDto>>
    {
    }

    public class GetHealthCheckServicesInfoQueryHandler : IRequestHandler<GetHealthCheckServicesInfoQuery, IEnumerable<ServicesInfoDto>>
    {
        public async  Task<IEnumerable<ServicesInfoDto>> Handle(GetHealthCheckServicesInfoQuery request, CancellationToken cancellationToken)
        {
            List<ServicesInfoDto>  servicesInfoDtos = new List<ServicesInfoDto>();
            ServicesInfoDto item = new ServicesInfoDto();

            item.ServiceUrl=@"MarketData/total-number-Of-shares-traded";
            item.ServiceName="ریست شدن حجم  معاملات نمادها ";
            servicesInfoDtos.Add(item);

            ServicesInfoDto item1 = new ServicesInfoDto();
            item1.ServiceUrl=@"MarketData/total-trade-value";
            item1.ServiceName="ریست شدن ارزش معاملات نمادها";
            servicesInfoDtos.Add(item1);


            ServicesInfoDto item2 = new ServicesInfoDto();
            item2.ServiceUrl=@"MarketData/Price-variation";
            item2.ServiceName="ریست شدن تغییر آخرین قیمت";
            servicesInfoDtos.Add(item2);

            ServicesInfoDto item3 = new ServicesInfoDto();
            item3.ServiceUrl=@"MarketData/closing-price-percent";
            item3.ServiceName="ریست شدن درصد تغییر آخرین قیمت";
            servicesInfoDtos.Add(item3);

            ServicesInfoDto item4 = new ServicesInfoDto();
            item4.ServiceUrl=@"MarketData/state-change";
            item4.ServiceName="بروز بودن وضعیت نمادها";
            servicesInfoDtos.Add(item4);

            ////////////////////////////////////////////////////////////ts//////////////////////////////////////////////////////
            ServicesInfoDto item5 = new ServicesInfoDto();
            item5.ServiceUrl=@"MarketDataFromTse/close-price-compare-with-Tse";
            item5.ServiceName="بروز بودن قیمت  دیروز نمادها";
            servicesInfoDtos.Add(item5);

            ServicesInfoDto item6 = new ServicesInfoDto();
            item6.ServiceUrl=@"MarketDataFromTse/traded-price-compare-With-tse";
            item6.ServiceName="بروز بودن آخرین قیمت نمادها  ";
            servicesInfoDtos.Add(item6);

            ServicesInfoDto item7 = new ServicesInfoDto();
            item7.ServiceUrl=@"MarketDataFromTse/close-price-compare-with-Tse";
            item7.ServiceName="بروز بودن قیمت پایانی نمادها";
            servicesInfoDtos.Add(item7);

            //-------------------------------------------------Webservice-------------------------------------------------------


            ServicesInfoDto item8 = new ServicesInfoDto();
            item8.ServiceUrl=@"WebServicesConnectionChecker/all-webservices-info";
            item8.ServiceName="سلامت وب سرویس ها ";
            servicesInfoDtos.Add(item8);


            return servicesInfoDtos.ToList();
        }
    }
}
