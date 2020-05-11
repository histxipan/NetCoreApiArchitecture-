using System;
using System.Collections.Generic;
using System.Text;
using WebApiNinjectStudio.Domain.Entities;

namespace WebApiNinjectStudio.Domain.Abstract
{
    public interface IApiUrlRepository
    {
        IEnumerable<ApiUrl> ApiUrls { get; }
        int SaveApiUrl(ApiUrl apiUrl);
        int DelApiUrl(int apiUrlId);
    }
}
