namespace Domain.SearchParameters
{
    public class SearchParameters
    {
        public int StartIndex { get; private set; }
        public int TotalPerPages { get; private set; }
        public int End { get { return StartIndex + TotalPerPages; } }

        public static SearchParameters CreateSearchParameters(int startIndex, int totalPerPages) 
        {
            return new SearchParameters(startIndex,totalPerPages);
        }

        public SearchParameters(int startIndex, int totalPerPages)
        {
            StartIndex = startIndex;
            TotalPerPages = totalPerPages;
        }

        public void NextPage() 
        {
            StartIndex += TotalPerPages;
        }
    }
}
