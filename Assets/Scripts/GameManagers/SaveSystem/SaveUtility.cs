public static class SaveUtility
{
    public static readonly string[] SaveSlots = {"saveSlot1", "saveSlot2", "SaveSlot3"};

    public static string[] GetSaveNames()
    {
        var names = new string[SaveSlots.Length];

        for (int i = 0; i < SaveSlots.Length; i++)
        {
            ES3.Load("Name", SaveSlots[i], names[i]);
        }

        return names;
    }
}
