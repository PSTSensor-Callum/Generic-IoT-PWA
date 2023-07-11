using Generic_IoT_PWA.Models.Abstracts.Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using Generic_IoT_PWA.Models;
using Generic_IoT_PWA.Models.Interfaces;
using Generic_IoT_PWA.Models.Abstracts.Dtos;

namespace Generic_IoT_PWA.Data.Extensions
{

    public record PageData<T>(IQueryable<T> Data, int Page, int PageSize, int PageCount, int TotalDataCount, string? PreviousPage, string? NextPage)
    where T : Entity;

    public static class PaginationExtensions
    {
        public static PageData<T> GetPageData<T>(this IQueryable<T> data, int pageSize, int page, HttpRequest request) where T : Entity
        {
            int totalDataCount = data.Count();

            page = page < 0 ? 1 : page; // setting to first page if lower than 1

            int maxPages = pageSize > 0 && totalDataCount > pageSize ? (int)Math.Ceiling((float)totalDataCount / (float)pageSize) : 1;
            page = page > maxPages ? maxPages : page; // setting to last page if specified page is beyond the amount of pages available

            if (pageSize > 0 && totalDataCount > 0) // if pageSize is 0, no pagination happens
            {
                // skipping previous pages and only taking the next n items determined by the pageSize
                int itemsToSkip = pageSize * (page - 1);
                data = data.Skip(itemsToSkip).Take(pageSize);
            }

            // creating the urls for previous and next pages if they exist
            var basePageUrl = UriHelper.GetDisplayUrl(request);
            basePageUrl = Regex.Replace(basePageUrl, "[&?]page=\\d*", ""); // remove the page parameter from URL
            basePageUrl = basePageUrl.Contains('?') ? $"{basePageUrl}&" : $"{basePageUrl}?"; // pre-appending '?' if no parameters are left, '' otherwise

            var previousPage = page <= 1 ? null : $"{basePageUrl}page={page - 1}";
            var nextPage = page >= maxPages ? null : $"{basePageUrl}page={page + 1}";

            return new(data, page, pageSize, maxPages, totalDataCount, previousPage, nextPage);
        }

        public static Pagination<D> GetPaginatedData<T, D>(this IQueryable<T> data, int pageSize, int page, HttpRequest request)
        where T : Entity, IDtoable<D>
        where D : Dto
        {
            PageData<T> pageData = data.GetPageData(pageSize, page, request);
            List<D> processedData = pageData.Data?.Any() ?? false ? pageData.Data.AsEnumerable().Select(x => x.ToDto()).ToList() : new();

            return new(processedData, pageData.TotalDataCount, pageData.Page, pageData.PageCount, pageData.PageSize, pageData.PreviousPage, pageData.NextPage);
        }
    }
}
