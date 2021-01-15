public static class SaveUtility
{
    public static readonly string[] SaveSlots = {"saveSlot 1", "saveSlot 2", "SaveSlot 3"};

    public static string[] GetSaveNames()
    {
        var names = new string[SaveSlots.Length];

        for (int i = 0; i < SaveSlots.Length; i++)
        {
            names[i] = ES3.Load("Name", SaveSlots[i], $"Slot {i + 1}: Empty");
        }

        return names;
    }
}
