
# DEPRECATED
- We are no longer supporting this project.


SquareSix.Core - Re-usable code and helper classes between Xamarin.Forms applications. See the demo project for examples. 


## Setup

- In your App.cs class, add the following line of code.
```
SquareSixCore.Init();
```


## Package Includes
```
- SimpleIOC: A simple container for registering and retreiving services.
- AsyncCommand: Asynchronous commands for your command bindings.
- BaseViewModel: InitAsync overide method, Title, and IsBusy property.
- BaseListViewModel: Handles ListViews/CollectionViews, paging, ItemSource, PullToRefresh, and Row selection etc.
- BasePropertyChangedModel: Based off of FodyWeavers and auto implements INotifyPropertyChanged on properties.
- ContentViewExtensions: AttachLifecycleToPage (OnAppearing, OnDisappearing).
- HttpExtensions: Used in the RestService and making http requests.
- RequestUtils: AddJsonContent, AppendQueryParameter, AppendUrlSegment, and Combine methods. Used for HttpRequests. Using this service to make requests will output the Http request data to the console.
- Converters: InverseBoolConverter.
- EventToCommandBehavior: Allows binding xamarin controls EventHandlers to a command in the ViewModel.
```


## ISimpleIOC Interafce methods
```
bool ContainsKey<T>();
void Register<T>(T service);
void Register<T>() where T : new();
void Register<T>(Func<T> function);
void Register(Type type, object service);
void Register(Type type, Func<object> function);
void Clear();
void Unregister<T>();
T Resolve<T>(bool nullIsAcceptable = false);
object Resolve(Type type, bool nullIsAcceptable = false);
```


## IAlertService Interafce methods
```
Task<bool> ShowConfirmationAsync(string message, string title = "", string okText = "OK", string cancel = "Cancel");
Task ShowAlertAsync(string title, string message, string okText = "OK");
Task<string> ShowPromptAsync(string title, string message, string okText = "OK", string cancel = "Cancel", string placeholder = null, int maxLegnth = -1, Keyboard keyboard = null, string initialValue = null);
Task<string> ShowActionSheetAsync(string title, string cancel, string desctruction, params string[] buttons);
```


## IRestService Interafce methods
```
Task<RestResponse<T>> PrepareAndSendRequest<T>(HttpMethod httpMethod, Uri uri, object data, CancellationToken cancellationToken, bool addAuthHeader = true, string contentType = "application/json");
```


## IAsyncCommand Interafce methods
```
Task ExecuteAsync();
bool CanExecute();
void RaiseCanExecuteChanged();
```


## IAuthorizationHeaderService Interafce methods
```
Task<List<KeyValuePair<string, string>>> GetAuthorizationHeaders();
```

- Implement this interface, and add it to the SimpleIOC container, if you want to add Authorization headers to your HttpRequestMessage's. If the `addAuthHeader` argument is true when calling the `PrepareAndSendRequest`, then the `RestService` will call this implementation and try to popluate the headers for you.

