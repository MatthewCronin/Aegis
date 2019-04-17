using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aegis
{
    public class GetSecQuestionsResult
    {
        [JsonProperty("GetSecQuestionsResult")]
        public List<SecurityQuestions> secQuestions { get; set; }
    }
    public class SecurityQuestions
    {
        [JsonProperty("SecQuestionID")]
        public int SecQuestionID { get; set; }
        [JsonProperty("SecQuestion")]
        public string SecQuestion { get; set; }
    }
}