# Mitchell

## Vehicle Repository

RESTful web service that performs CRUD operations (Create, Read, Update, and Delete) for a
Vehicle entity.

#### Service Endpoint URL

+ https://azurevehicle.azurewebsites.net/

## Requirments

+ C# or Java. C# is preferred.

+ Automated testing.

+ In-memory persistence of created vehicle objects.

+ Function properly with the provided test web client.

## Optional Features

#### Implemented

+ Add validation to your service.

   Vehicles must have a non-null / non-empty make and model specified, and the year must be between 1950 and 2050.

#### Not Implemented

+ Add filtering to your service.

   The GET vehicles route should support filtering vehicles based on one or more vehicle properties. (EX: retrieving all vehicles where the ‘Make’ is ‘Toyota’)

+ Write an example client for your service!

   Client should leverage AngularJS. (1.x or 2.0) Any other libraries used are entirely up to you!
   
## Design
 
+ A Vehicle is a simple object defined as follows:
```
   public class Vehicle
   {
       public intId { get; set; }
       public int Year { get; set; }
       public string Make { get; set; }
       public string Model { get; set; }
   }
```

+ RESTful service must implement the following routes:
```
   GET vehicles
   GET vehicles/{id}
   POST vehicles
   PUT vehicles
   DELETE vehicles/{id}
```
