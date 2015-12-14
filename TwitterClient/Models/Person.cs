namespace TwitterClient
{
    public class Person
    {
        private string name;
        private string screenName;
        private string profilePictureURL;

        public Person(string name, string screenName, string profilePictureURL)
        {
            this.Name = name;
            this.screenName = screenName;
            this.ProfilePictureURL = profilePictureURL;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ProfilePictureURL
        {
            get { return profilePictureURL; }
            set { profilePictureURL = value; }
        }

        public override string ToString()
        {
            return string.Concat(name," (@",screenName,")");
        }
    }
}