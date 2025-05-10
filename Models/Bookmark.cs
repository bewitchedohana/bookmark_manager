using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookmarkManager.Models;

public sealed class Bookmark : BaseModel, INotifyPropertyChanged {
	private string _url = string.Empty;

	public string Url {
		get => _url;
		set {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
			_url = value;
			NotifyPropertyChanged();
		}
	}

	private string _name = string.Empty;    

    public string Name {
		get => _name;
		set {
			ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
			_name = value;
            NotifyPropertyChanged();
        }
	}

    public event PropertyChangedEventHandler? PropertyChanged;

    public Bookmark() : base() { }

    public Bookmark(string url) : this() {
        Url = url;
    }

    public Bookmark(string url, string name) : this(url) {
        Name = name;
    }

	private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") 
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
