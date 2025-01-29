using Newtonsoft.Json;

public class BoardResponse
{
    [JsonProperty("boards")]
    public List<Board> Boards { get; set; }
}

public class Board
{
    [JsonProperty("items_page")]
    public ItemsPage ItemsPage { get; set; }
}

public class ItemsPage
{
    [JsonProperty("cursor")]
    public string Cursor { get; set; }

    [JsonProperty("items")]
    public List<Item> Items { get; set; }
}

public class Item
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("group")]
    public Group Group { get; set; }

    [JsonProperty("column_values")]
    public List<ColumnValue> ColumnValues { get; set; }
}

public class Group
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
}

public class ColumnValue
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("index")]
    public string Index { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("label_style")]
    public LabelStyle LabelStyle { get; set; }
}


public class LabelStyle
{
    [JsonProperty("border")]
    public string Border { get; set; }

    [JsonProperty("color")]
    public string Color { get; set; }
}