namespace ServiceLayer
{
    public class BlogInformation
    {
        public int BlogId { get; set; }
        public byte[] BlogImage { get; set; }
        public string BlogName { get; set; }
        public int PostCount { get; set; }
        public int FollowingCount { get; set; }
        public bool IsFollowed { get; set; }
    }
}
