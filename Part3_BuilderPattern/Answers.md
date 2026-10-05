# Part 3 - Builder Pattern: Written Answers

## Task 3.1

### Q1. Why is a single 20-parameter constructor a problem in practice?
- first because the constructor will be too long to read, and it tells the
  reader nothing, they should open the class to see what each pramameter mean.
- the caller must pass the null or 0 values if they don't care about
- adding one more property force you to edit every caller throught the code


### Q2. Is it purely a "constructor too long" problem?
- No, it more deeper, because it seprate domains inside one class.. and that makes
it low-cohesion.

## Task 3.3 - Why composed builders are better than one big builder

**Single Reposiniliyt** : 'addressBuildder' owns only adress data and adress
rules it doen'st touch any other class.. and the same goes to 'orderbuilder',
and the invoicebuilder owns only the composition, like id and customer etc.. in
a single builder everything is tighed

**Independent validation.** `AddressBuilder.Build()` guarantees a complete `Address` by itself. The parent never needs to know that zip codes or cities exist; it just receives a valid `Address`. The same holds for `OrderBuilder` / `OrderDetails`. An invalid `Address` or `OrderDetails` can't exist anywhere in the program, and each builder can be unit-tested in isolation.


**Independent validation**: each builder gruarantees a completes object by
itself and the parent knows nothing about it and donesn;'t need that it just
receives a valid object.

