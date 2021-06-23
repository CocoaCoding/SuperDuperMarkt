# SuperDuperMarkt
## C# employment test

The customer SuperDuperMarkt requires an application for the management of products, their prices, their quality and, if necessary, their expiry dates.
A product is usually registered with the application when it is put on the shelf and then and then changes its quality and, if necessary, price on a daily basis. When a product falls below 
quality level or when the expiration date is reached, the product is removed from the shelf or product is removed from the shelf or disposed of. The application is designed to 
help shelf managers to price out products and to remove old products from the shelves in time shelves in time, so that there are no more low-quality products on the shelves!

1.) The ProductBase class

The most important data model of the application is the ProductBase class. It provides the properties Description (name), PriceBase (base price), 
QualityBase (basic quality) and ExpireDate (expiry date).  In addition, the ProductBase class has the CreationDate property, which is currently only needed for products of the type
of type cheese. Cheese loses one quality point every day. To be able to calculate the quality for any day in the future, it is 
it is necessary to know when the item was added to the assortment. The only other date in the class is the expiration date, but would
the cheese start losing quality only after this date, it would be sorted out before its quality changes.

The ProductBase class provides two abstract methods. GetProductType returns a string and is used to identify the product type when saving and loading products as CSV.
identification of the product type. The method CreateProductDaily contains the processing rules. When called, the method requires a date
as a parameter and returns an object of the ProductDaily type. 

ProductBase is the parent class for three other classes. WineProduct, CheeseProduct and BreadProduct.

The abstract parent class requires the child classes to implement the GetProductType and CreateProductDaily methods themselves. Thus
each type of product its own processing rules.

The properties of a product object cannot be changed after instantiation. All information about future quality or
price is provided by the ProductDaily class.

Before output, objects of the ProductViewModel class are created from the products.
This serves the sole purpose of formatting the price and date in an appealing way. 

2.) The ProductDaily class

The ProductDaily class manages information for a product for a specific day, that is, with the daily price and quality. These are the properties 
PriceDaily and QualityDaily. The boolean value IsSortedOut indicates whether a product should be sorted out. Furthermore there is the property SortedOutReason in which the
property specifies why a product has to be sorted out.

The ProductDailyViewModel class, similar to the ProductViewModel class, provides a better formatted output of the ProductDaily.

3.) Data management

All products are managed in an instance of the ProductRepository class.

In the constructor, ProductRepository requires an object of a class that implements the IProductLoaderInterface interface. This makes the methods LoadProducts,
SaveProducts and DefaultProducts, which are called by ProductRepository. Currently only the FileProductLoader class implements the 
IProductLoaderInterface. It loads the products from a CSV file. With this design it is possible to use alternative ProductLoaders. For example to load the 
products from a database or from a web service. Only another ProductLoader has to be passed to the ProductRepository. 
If no CSV file is found, the products from the DefaultProducts method are used.

The Repository Pattern would make it possible to filter or sort the products at a central location. Neither happens in the application so far.

4.) Processing

The entry point of the application is the Run method of the SuperDuperMarktProgram class.
The SuperDuperMarktProgram class was designed in the singleton pattern. Only one instance of the class can be created at runtime.

In the first step, all products are loaded into the ProductRepository. This is done, as mentioned before, with the help of the FileProductLoader. Afterwards all 
products are output with their basic values. The method CreateProductDaily creates the product information between a period of a start date and an 
an end date. In two loops all products and all days in the period are run through and for the products the method CreateProductDaily is called. 
Then the output of the created ProductDaily is done for all products over all days.

All output on the console is the task of the class ConsoleWriter.

5.) Notes

Theoretically, it would be conceivable to outsource the ProductBase class and its child classes and ViewModels to a separate library. Then it would be possible
to exchange the processing rules independently from the main program. Unfortunately, with the current structure it is not possible to add additional
to add further products in this way. The FileProductLoader class, which is used to load data from a CSV file, must know all product classes.

