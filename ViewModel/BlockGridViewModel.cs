using NetCore.Models;
using System.Dynamic;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Common.UmbracoContext;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Umbraco.Cms.Web.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Umbraco.Cms.Core.Constants.HttpContext;
using static Umbraco.Cms.Core.Constants.WebhookEvents;
using System.Diagnostics.Metrics;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using System.Collections.Generic;
using Lucene.Net.Util;

namespace Custom.ViewModel
{
    public class BlockGridViewModel<T>
    {
        public BlockGridViewModel()
        {

        }
        public BlockGridViewModel(BlockGridItem Model, UmbracoHelper Umbraco, string ModelTypeAlias, IQueryCollection query)
        {
            try
            {
                Pager = new Pager();

                #region query string

                if (query != null)
                {
                    Pager.Keyword = query["Keyword"].ToStringNull();

                    var rpage = query["Page"].ToStringNull().ToInt32();
                    Pager.Page = rpage == 0 ? 1 : rpage;
                }

                #endregion

                if (Model != null)
                {
                    #region widget paramters
                    
                    var maxRecords = 0;
                    var pageSize = 9;
                    var paths = new List<int>();

                    try
                    {
                        Pager.PagerName = ModelTypeAlias;

                        maxRecords = Model.Content.Value("maxRecords").ToInt32_Count();
                        pageSize = Model.Content.Value("pageSize").ToInt32_Count2();
                        paths = Model.Content.Value("listPaths").ToListIntger();

                        Pager.MaxRecords = maxRecords;
                        Pager.PageSize = pageSize;
                        Pager.Paths = paths;

                        Pager.PageUdi = Model?.ContentUdi?.ToStringNull();
                    }
                    catch (Exception ex)
                    {
                        SharedHelper.LogException(ex);
                    }

                    #endregion

                    #region get list

                    //// var BlockListModel = (BlockListModel)Model?.Content?.Value("List");
                    //// var list = new List<Elm_TrainingFacilitiesItem>();
                    //// foreach (var item in BlockListModel)
                    //// {
                    ////     list.Add((Elm_TrainingFacilitiesItem)item.Content);
                    //// }
                    //// list = list.OrderByDescending(p => p.Value("pageID")).ToList();

                    // if (paths.Count == 0)
                    // {
                    //     paths.Add(Model.Content.Id);
                    //     var cardID = Umbraco.Content(Model.Content.Id)?
                    //                 .ChildrenOfType(Dt_Cards.ModelTypeAlias)?
                    //                 .Where(x => x.IsVisible())
                    //                 .FirstOrDefault();
                    //     if (cardID != null)
                    //     {
                    //         paths.Add(cardID.Id);
                    //     }
                    // }

                    List = new List<T>();

                    if (!string.IsNullOrEmpty(ModelTypeAlias) && paths?.Count > 0)
                    {
                        foreach (var item in paths)
                        {
                            try
                            {
                                var items = Umbraco.Content(item)?
                                                                    .ChildrenOfType(ModelTypeAlias)?
                                                                    //.Children<Dt_TrainingFacilitiesItem>()
                                                                    .Where(x => x.IsVisible())
                                                                    //.Where(new Func<IPublishedContent, bool>())
                                                                    //.OrderBy(p => p.Value<int>("Order"))
                                                                    .OrderBy(p => p.Value<int>("Order"))
                                                                    //.Take(maxRecords)
                                                                    //.Take(200)
                                                                    //.ToList()
                                                                    ;

                                if (items?.Count() > 0)
                                {
                                    //List.AddRange((IEnumerable<T>)items);
                                    foreach (var item2 in items)
                                    {
                                        List.Add((T)item2);
                                    }
                                }

                                //if (items?.Count() > 0)
                                //{
                                //    List.AddRange((List<T>)items);
                                //}

                                //if (List?.Count >= pageSize)
                                //{
                                //    break;
                                //}
                            }
                            catch (Exception ex)
                            {
                                SharedHelper.LogException(ex);
                            }
                        } 
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                SharedHelper.LogException(ex);
            }
        }
        public BlockGridViewModel(IPublishedContent Model, UmbracoHelper Umbraco, string ModelTypeAlias, IQueryCollection query)
        {
            try
            {
                Pager = new Pager();

                #region query string

                if (query != null)
                {
                    Pager.Keyword = query["Keyword"].ToStringNull();

                    var rpage = query["Page"].ToStringNull().ToInt32();
                    Pager.Page = rpage == 0 ? 1 : rpage;
                }

                #endregion

                if (Model != null)
                {
                    #region widget paramters

                    var maxRecords = 0;
                    var pageSize = 9;
                    var paths = new List<int>();

                    try
                    {
                        Pager.PagerName = ModelTypeAlias;

                        maxRecords = Model.Value("maxRecords").ToInt32_Count();
                        pageSize = Model.Value("pageSize").ToInt32_Count2();
                        paths = Model.Value("listPaths").ToListIntger();

                        // add current page id
                        paths.Add(Model.Id);

                        Pager.MaxRecords = maxRecords;
                        Pager.PageSize = pageSize;
                        Pager.Paths = paths;

                        Pager.PageUdi = Model.Key.ToStringNull();
                    }
                    catch (Exception ex)
                    {
                        SharedHelper.LogException(ex);
                    }
                    
                    #endregion

                    #region get list

                    List = new List<T>();

                    if (!string.IsNullOrEmpty(ModelTypeAlias) && paths?.Count > 0)
                    {
                        foreach (var item in paths)
                        {
                            try
                            {
                                var items = Umbraco.Content(item)?
                                                                    .ChildrenOfType(ModelTypeAlias)?
                                                                    //.Children<Dt_TrainingFacilitiesItem>()
                                                                    .Where(x => x.IsVisible())
                                                                    //.OrderBy(p => p.Value<int>("Order"))
                                                                    .OrderBy(p => p.Value<int>("Order"))
                                                                    //.Take(maxRecords)
                                                                    //.Take(200)
                                                                    //.ToList()
                                                                    ;

                                if (items?.Count() > 0)
                                {
                                    //List.AddRange((IEnumerable<T>)items);
                                    foreach (var item2 in items)
                                    {
                                        List.Add((T)item2);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                SharedHelper.LogException(ex);
                            }
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                SharedHelper.LogException(ex);
            }
        }


        public void SetPager()
        {
            try
            {
                if (List != null)
                {
                    if (Pager.PageSize > 0)
                    {
                        var totalPages = (int)Math.Ceiling((double)(List?.Count() ?? 0) / (double)Pager.PageSize);

                        if (Pager.Page > totalPages)
                        {
                            Pager.Page = totalPages;
                        }
                        else if (Pager.Page < 1)
                        {
                            Pager.Page = 1;
                        }

                        Pager.TotalPages = totalPages;

                        List = List?.Skip((Pager.Page - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();
                    }

                    if (Pager.MaxRecords > 0)
                    {
                        List = List?.Take(Pager.MaxRecords).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                SharedHelper.LogException(ex);
            }
        }
        public List<T> SetPager(List<T> List2, Pager Pager2)
        {
            try
            {
                if (Pager2.PageSize > 0)
                {
                    var totalPages = (int)Math.Ceiling((double)(List2?.Count() ?? 0) / (double)Pager2.PageSize);

                    if (Pager2.Page > totalPages)
                    {
                        Pager2.Page = totalPages;
                    }
                    else if (Pager2.Page < 1)
                    {
                        Pager2.Page = 1;
                    }

                    Pager2.TotalPages = totalPages;

                    List2 = List2?.Skip((Pager2.Page - 1) * Pager2.PageSize).Take(Pager2.PageSize).ToList();
                }

                if (Pager2.MaxRecords > 0)
                {
                    List2 = List2?.Take(Pager2.MaxRecords).ToList();
                }

                //List = List2;
                //Pager = Pager2;
            }
            catch (Exception ex)
            {
                SharedHelper.LogException(ex);
            }

            return List2;
        }
        public void GetChildren(UmbracoHelper Umbraco, IPublishedValueFallback PublishedValueFallback)
        {
            try
            {
                #region get list

                foreach (var item in Pager.Paths)
                {
                    try
                    {
                        List<string> types = new List<string>();

                        types.Add(Dt_PageContent.ModelTypeAlias);
                        types.Add(Dt_Folder.ModelTypeAlias);
                        types.Add(Dt_News.ModelTypeAlias);
                        types.Add(Dt_NewsItem.ModelTypeAlias);
                        types.Add(Dt_Events.ModelTypeAlias);
                        types.Add(Dt_EventsItem.ModelTypeAlias);
                        //types.Add(Dt_Services.ModelTypeAlias);
                        //types.Add(Dt_ServicesItem.ModelTypeAlias);
                        //types.Add(Dt_TrainingFacilities.ModelTypeAlias);
                        //types.Add(Dt_TrainingFacilitiesItem.ModelTypeAlias);
                        //types.Add(Dt_AlbumPhotos.ModelTypeAlias);
                        //types.Add(Dt_AlbumPhotosItem.ModelTypeAlias);
                        //types.Add(Dt_AlbumVideos.ModelTypeAlias);
                        //types.Add(Dt_AlbumVideosItem.ModelTypeAlias);

                        #region old

                        ////var test = SearchHelper.SearchRecursive()
                        //List<string> ids = new List<string>();
                        //// Code for searching in certain city by id (rootId)
                        //ids = index
                        //       .Searcher
                        //       .CreateQuery("content")
                        //       .ParentId(rootId)   //This returns only first child level
                        //       .And()
                        //       .GroupedOr(new[] { "__NodeTypeAlias" }, currentCitySearchTypes)
                        //       .And()
                        //       .ManagedQuery(query)
                        //       .Execute().Select(x => x.Id).ToList();

                        //// And additional search for all other elements in the root(Blog etc.)
                        //ids.AddRange(index
                        //              .Searcher
                        //              .CreateQuery("content")
                        //              .ManagedQuery(query)
                        //              .And()
                        //              .GroupedOr(new[] { "__NodeTypeAlias" }, rootTypes)
                        //              .Execute().Select(x => x.Id).ToList());

                        //var items = Umbraco.Content(item)?
                        //                        .Children()
                        //                        //.ChildrenOfType(types)
                        //                        //.Where(x =>
                        //                        //x.IsVisible(PublishedValueFallback)
                        //                        // &&
                        //                        // types.Contains(x.ContentType.Alias)
                        //                        //)
                        //                         // .Where(x => x.Value<Boolean>("ShowInMainMenu") == true)
                        //                         .Where(p => p.Name.ToLower().Contains(Pager.Keyword.ToLower()))
                        //                        .ToArray();

                        #endregion

                        #region MyRegion

                        string keyword = Pager.Keyword.ToLower();

                        //IPublishedContent[] items = null;
                        //List<IPublishedContent> items_All = new List<IPublishedContent>();

                        var items = Umbraco.Content(item)?.Children()
                                    .Where(x => x.IsVisible(PublishedValueFallback)
                                    && types.Contains(x.ContentType.Alias)
                                    //&& WhereCustom(x, keyword)
                                    )
                                    //.Where(x => x.Value<Boolean>("ShowInSiteMap") == true)
                                    .ToArray();

                        if (items?.Count() > 0)
                        {
                            //items_All.AddRange(items);
                            GetChildrenDetails(items, types, keyword, PublishedValueFallback);

                            //items_All.AddRange(items);

                            //foreach (var level1 in items)
                            //{
                            //    var children = level1.Children
                            //                    .Where(x => x.IsVisible(PublishedValueFallback) && types.Contains(x.ContentType.Alias))
                            //                    //.Where(x => x.Value<Boolean>("ShowInSiteMap") == true)
                            //                    .ToArray();

                            //    if (children.Length > 0)
                            //    {
                            //        items_All.AddRange(children);
                            //    }
                            //}
                        }

                        #endregion

                        if (items_All?.Count() > 0)
                        {
                            IEnumerable<IPublishedContent> result = items_All
                                .Where(x => WhereCustom(x, keyword))
                                .OrderByDescending(x => Fitness(x.Name, keyword))
                                ;

                            if (items_All?.Count() > 0)
                            {
                                List.AddRange((IEnumerable<T>)result);
                            }

                            ////string keyword = Pager.Keyword.ToLower();
                            ////string[] searchTerms = new string[] { Pager.Keyword.ToLower() };

                            //// Get the filtered list and sort by count of matching characters
                            //IEnumerable<IPublishedContent> result = items_All
                            //    .Where(x =>
                            //    x.Name.ToLower().StartsWith(keyword, StringComparison.InvariantCultureIgnoreCase)
                            //    || x.Name.ToLower().Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                            //    || x.Name.ToLower().Equals(keyword, StringComparison.InvariantCultureIgnoreCase)
                            //    || x.ValueOrDefault("pageTitle", defaultValue: "mxmxmx").Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                            //    || x.ValueOrDefault("details", defaultValue: "mxmxmx").Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                            //    )
                            //    .OrderByDescending(x => Fitness(x.Name, keyword));

                            ////var result = items_All.Select(x => new
                            ////                        {
                            ////                            Customer = x,
                            ////                            //MatchEvaluation = (searchTerms.Contains(x.Name.ToLower()) ? 1 : 0),
                            ////                            MatchEvaluation = (x.Name.ToLower().Contains(keyword) || x.Name.ToLower() == keyword ? 1 : 0),
                            ////                        })
                            ////                        .OrderByDescending(x => x.MatchEvaluation)
                            ////                        .Select(x => x.Customer)
                            ////                        ;

                            //if (result?.Count() > 0)
                            //{
                            //    List.AddRange((IEnumerable<T>)result);

                            //    //List = List.Take(20).ToList();

                            //    //Pager.Paths = new List<int>();
                            //    //Pager.Paths.AddRange(result.Select(p => p.Id));
                            //    //GetChildren(Umbraco, PublishedValueFallback);
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        SharedHelper.LogException(ex);
                    }
                }

                #endregion

                //children = item.Children?
                //                                .Where(x => x.IsVisible(PublishedValueFallback) && types.Contains(x.ContentType.Alias))
                //                                .Where(x => x.Value<Boolean>("ShowInMainMenu") == true)
                //                                .ToArray();

            }
            catch (Exception ex)
            {
                SharedHelper.LogException(ex);
            }

            //return List2;
        }
        private void GetChildrenDetails(IPublishedContent[] items, List<string> types, string keyword, IPublishedValueFallback PublishedValueFallback)
        {
            try
            {
                if (items?.Count() > 0)
                {
                    if (items_All == null)
                    {
                        items_All = new List<IPublishedContent>();
                    }

                    //items_All.AddRange(items);

                    foreach (var level1 in items)
                    {
                        items_All.Add(level1);

                        var children = level1.Children
                                        .Where(x => x.IsVisible(PublishedValueFallback)
                                        && types.Contains(x.ContentType.Alias)
                                        //&& WhereCustom(x, keyword)
                                        )
                                        //.Where(x => x.Value<Boolean>("ShowInSiteMap") == true)
                                        .ToArray();

                        if (children.Length > 0)
                        {
                            //items_All.AddRange(children);
                            GetChildrenDetails(children, types, keyword, PublishedValueFallback);
                            //foreach (var level2 in children)
                            //{
                            //    //items_All.Add(level1);
                            //    var children2 = level2.Children
                            //                    .Where(x => x.IsVisible(PublishedValueFallback)
                            //                    && types.Contains(x.ContentType.Alias)
                            //                    )
                            //                    //.Where(x => x.Value<Boolean>("ShowInSiteMap") == true)
                            //                    .ToArray();

                            //    if (children2.Length > 0)
                            //    {
                            //        items_All.AddRange(children2);
                            //        //GetChildrenDetails(children2, types, PublishedValueFallback);
                            //        foreach (var level3 in children2)
                            //        {
                            //            //items_All.Add(level1);
                            //            var children3 = level3.Children
                            //                            .Where(x => x.IsVisible(PublishedValueFallback)
                            //                            && types.Contains(x.ContentType.Alias)
                            //                            )
                            //                            //.Where(x => x.Value<Boolean>("ShowInSiteMap") == true)
                            //                            .ToArray();

                            //            if (children3.Length > 0)
                            //            {
                            //                items_All.AddRange(children3);
                            //                //GetChildrenDetails(children3, types, PublishedValueFallback);
                            //            }
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SharedHelper.LogException(ex);
            }
            //return items_All;
        }


        static bool WhereCustom(IPublishedContent x, string keyword)
        {
            try
            {
                return
                        x.Name.ToLower().StartsWith(keyword, StringComparison.InvariantCultureIgnoreCase)
                    || x.Name.ToLower().Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                    || x.Name.ToLower().Equals(keyword, StringComparison.InvariantCultureIgnoreCase)
                    || x.ValueOrDefault("pageTitle", defaultValue: "mxmxmx").Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                    || x.ValueOrDefault("details", defaultValue: "mxmxmx").Contains(keyword, StringComparison.InvariantCultureIgnoreCase);

            }
            catch (Exception ex)
            {
                SharedHelper.LogException(ex);
            }
            return false;
        }
        static int Fitness(string individual, string target)
        {
            try
            {
                return Enumerable.Range(0, Math.Min(individual.Length, target.Length))
                             .Count(i => individual[i] == target[i]);
            }
            catch (Exception ex)
            {

            }
            return 0;
        }


        public List<IPublishedContent> items_All { get; set; }
        public List<T> List { get; set; }
        public Pager Pager { get; set; }

        //public dynamic Paramters { get; set; } = new DynamicParamters();

        //// Usage
        //dynamic person = new DynamicParamters();
        //person.Name = "John Doe";
        //Console.WriteLine("Name: " + person.Name);
    }
}
