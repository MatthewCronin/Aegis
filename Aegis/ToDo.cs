using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aegis
{
    public class GetToDosResult
    {
        [JsonProperty("GetToDosResult")]
        public List<ToDoClass> todo { get; set; }
    }
    public class GetToDoResult
    {
        [JsonProperty("GetToDoResult")]
        public ToDoClass todo { get; set; }
    }
    public class ToDoClass
    {
        [JsonProperty("ToDoID")]
        public int? ToDoID { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("StartDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("EndDate")]
        public DateTime EndDate { get; set; }
        [JsonProperty("CreatedBy")]
        public int? CreatedBy { get; set; }
        [JsonProperty("CreatedDate")]
        public DateTime? CreatedDate { get; set; }
        [JsonProperty("UpdatedBy")]
        public int? UpdatedBy { get; set; }
        [JsonProperty("UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
        [JsonProperty("DisabledBy")]
        public int? DisabledBy { get; set; }
        [JsonProperty("DisabledDate")]
        public DateTime? DisabledDate { get; set; }
        [JsonProperty("Disabled")]
        public bool Disabled { get; set; }
        [JsonProperty("Completed")]
        public bool Completed { get; set; }
    }
}