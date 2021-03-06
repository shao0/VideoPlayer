using Prism.Events;
using VideoPlayerLibrary.Models;

namespace VideoPlayerLibrary.Events
{
    public class UrlChangeSentEvent : PubSubEvent<FilmPlayInfo>
    {
    }
}
