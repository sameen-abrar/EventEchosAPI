namespace EventEchosAPI.Helpers
{
    public static class GenerateId
    {
        public static string MakeId()
        {
            return Guid.NewGuid().ToString().Substring(0,10).ToUpper();
        }
    }
}
