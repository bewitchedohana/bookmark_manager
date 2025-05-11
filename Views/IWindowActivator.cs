namespace BookmarkManager.Views {
    internal interface IWindowActivator {
        T Activate<T>();
    }
}