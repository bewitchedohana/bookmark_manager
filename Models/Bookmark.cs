namespace BookmarkManager.Models;

public sealed class Bookmark : BaseModel {
	private string _url = string.Empty;

	public string Url {
		get => _url;
		set {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
			_url = value;
		}
	}

	private string _name = string.Empty;

	public string Name {
		get => _name;
		set {
			ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
			_name = value; 
		}
	}

    public Bookmark() : base() { }

    public Bookmark(string url) : this() {
        Url = url;
    }

    public Bookmark(string url, string name) : this(url) {
        Name = name;
    }
}
