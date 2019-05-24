using Newtonsoft.Json;
using System.Collections.Generic;

namespace SearchRepositoriesGitHUB.Models
{
    public class Search
    {
        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("incomplete_results")]
        public bool IncompleteResults { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
}
