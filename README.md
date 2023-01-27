# AssetManager

`Asset Manager` is a simple web application for managing office equipment, built with ASP.NET Core. You can see a live instance of it [here](https://assetmanager20221118085014.azurewebsites.net/).   
  
You will likely note that my approach is often overwrought for an app of this size--my intent has not been to implement a given feature with the lightest touch, but rather to improve and showcase my understanding of design patterns and best practices which are commonly used in enterprise applications.   
    
### Implemented features. 
<ul>
  <li>Data Persistence with EF Core & SQL Server</li>
  <li>Authentication & Authorization</li>
  <li>Caching of select field options, complete with recurring refresh via background service (merged but not deployed)</li>
  <li>AJAX forms with jQuery</li>
  <li>Custom controls with dynamic form data (Orders > Add Order > Dynamic Product + Count fields)</li>
</ul>

### Design patterns
<ul>
  <li>Model-View-Controller (MVC)</li>
  <li>Dependency Injection</li>
  <li>Service-Repository</li>
</ul>
