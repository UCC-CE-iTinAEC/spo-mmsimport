# spo-mmsimport
Simple console application for importing terms with custom properties into the Managed Metadata store in SharePoint Online/O365.

## Please Note
This is an extremely simple utility in its design and development. There are many things it does not do. It may be expanded in the future with additional capabilities but at this time it is in a fairly rudimentary state.

# Usage
The application expects a simple CSV file input with the following format:
```
Term,Prop_Custom1,Prop_Custom2,Prop_CustomN
Product A,Foo,Bar,Baz
Product B,Abc,Def,Ghi
```

Given that input, the utility will create terms in the term store using the Term column. Each column that starts with the prefix "Prop_" will be imported as custom properties on the term (the Prop_ prefix will be stripped before creating the property).  So for the input above, the following terms would be created:

* Product A (Custom1 = Foo, Custom2 = Bar, CustomN = Baz)
* Product B (Custom1 = Abc, Custom2 = Def, CustomN = Ghi)

## Example
```
SPOMMSImport.exe -f c:\myterms.csv --url https://mycompany.sharepoint.com/sites/mysite -u foo@mycompany.com -p 12345 --termGroup "Company Terms" --termSet "Products"
```

## Arguments
The utility expects the following arguments:

```
  -f, --file         Required. Data file containing data to import.

  -l, --url          Required. Url to the site or tenant

  -u, --username     Required. Username to authenticate

  -p, --password     Required. Password to authenticate

  -t, --termStore    Term store to import to.  If empty the default site 
                     collection term store will be used

  -g, --termGroup    Required. Term group containing the term set to import to.

  -s, --termSet      Required. Term set to import terms into.
```