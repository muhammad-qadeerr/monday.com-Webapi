using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System.Net.Http.Headers;

namespace ApiTestProject.Services
{
    public class MondayService
    {
        private readonly GraphQLHttpClient _graphQlHttpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MondayService> _logger;

        public MondayService(IConfiguration configuration, ILogger<MondayService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            _graphQlHttpClient = new GraphQLHttpClient("https://api.monday.com/v2/", new NewtonsoftJsonSerializer());
            _graphQlHttpClient.HttpClient.DefaultRequestHeaders.Authorization =
                AuthenticationHeaderValue.Parse(_configuration.GetValue<string>("MondayApiToken"));
            _graphQlHttpClient.HttpClient.DefaultRequestHeaders.Add("API-Version",
                _configuration.GetValue<string>("MondayBoardGraphQLApiVersion"));
        }

        public async Task<List<Item>> GetItemsFromBoard()
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = @"query { 
                        boards (ids: [3923377470]) { 
                            items_page (
                                limit: 1, 
                                query_params: { 
                                    rules: [
                                        { column_id: ""link7"", compare_value: [""4181:""], operator: contains_text }
                                    ]
                                }
                            ) { 
                                cursor 
                                items { 
                                    id 
                                    name 
                                    group { id title } 
                                    column_values { 
                                        id type text value 
                                        ... on StatusValue { 
                                            index text type label value 
                                            label_style { border color } 
                                        } 
                                    } 
                                } 
                            } 
                        } 
                    }",
                    Variables = new { }
                };

                GraphQLResponse<BoardResponse> graphQLResponse = await _graphQlHttpClient.SendQueryAsync<BoardResponse>(request);

                if (graphQLResponse.Errors != null && graphQLResponse.Errors.Length > 0)
                {
                    throw new Exception(graphQLResponse.Errors[0].Message);
                }

                return graphQLResponse.Data.Boards.FirstOrDefault()?.ItemsPage.Items ?? new List<Item>();
            }
            catch (GraphQLHttpRequestException ex)
            {
                if (ex.StatusCode != System.Net.HttpStatusCode.InternalServerError)
                    _logger.LogError(ex, $"MondayBoard GraphQL Exception in GetItemsFromBoard: {ex.StatusCode} => {ex.Content}");

                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "MondayBoard => Error while executing GetItemsFromBoard");
                throw;
            }
        }
    }
}
