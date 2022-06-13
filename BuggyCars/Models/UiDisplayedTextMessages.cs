using Newtonsoft.Json;

namespace BuggyCars.Models
{
    public class UiDisplayedTextMessages
    {
        [JsonProperty(PropertyName = "uiDisplayedMessages")]
        public UiDisplayedMessages[] UiDisplayedMessages { get; set; }
    }

    public class UiDisplayedMessages
    {
        [JsonProperty(PropertyName = "userAction")]
        public string UserAction { get; set; }

        [JsonProperty(PropertyName = "textMessage")]
        public string TextMessage { get; set; }
    }
}
