/// <summary>
/// Class used to store array of <c>LocalizationItem</c>s.
/// </summary>
[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}

/// <summary>
/// Class used to store language key and corresponding text value.
/// </summary>
[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}