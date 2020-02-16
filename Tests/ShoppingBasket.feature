Feature: ShoppingBasket

Background: 
# Consider passing the interface or concrete object name as argument
Given the basket database is empty
And there are no user subscribers
And a new basket is instantiated
And the following tax rules:
| Id | RuleName                |
| 1  | NoTax                   |
| 2  | Tax20Percent            |
| 3  | Administration5poundTax |
| 4  | BandedTax2020           |
And the following notification systems
| Id | NotificationSystemName  | CommunicationType | CommunicationChannel | Logger        |
| 1  | EmailNotificationSystem | email             | ..\..\..\..Emails    | ConsoleLogger |
| 2  | TextNotificationSystem  | text              | ..\..\..\..Phones    | ConsoleLogger |
And the following users
| Id | UserType | Email                | PhoneNumber |
| 1  | Customer | albert@hotmail.co.uk | 23855638    |
| 2  | Retailer | belle@yahoo.co.uk    | 86120183    |
| 3  | Customer | charlie@gmail.co.uk  | 41843576    |
| 4  | Retailer | diana@outlook.co.uk  | 03110894    |
| 5  | Customer | edward@aol.co.uk     | 84315325    |
And all notification systems subscribe to the basket

# INITIALISATION
Scenario: A newly constructed basket has zero items
Then the basket has 0 items

Scenario: The totals of a newly constructed basket are all zero
Then all totals are 0

# QUANTITIES
Scenario: Adding an item without an explicit quantity results in a quantity of 1 for the item
When the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | pasta |           |          |          |
Then the basket has 1 items
And the item 'pasta' has a quantity of 1

Scenario: After adding a single item to an empty basket, both the basket and item quantity are 1
When the following items are added:
| Id | Name | UnitPrice | Quantity | TaxRules |
| 1  | rice |           | 1        |          |
Then the basket has 1 items
And the item 'rice' has a quantity of 1

Scenario: After adding two items with different quantities to an empty basket, both the basket and item quantities are correct
When the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | bread |           | 3        |          |
| 2  | flour |           | 5        |          |
Then the basket has 2 items
And the item 'bread' has a quantity of 3
And the item 'flour' has a quantity of 5

Scenario: After adding a single item to the basket, adding the same item again will update the quantity of the previously added item
When the following items are added:
| Id | Name   | UnitPrice | Quantity | TaxRules |
| 1  | butter |           | 4        |          |
| 1  | butter |           | 2        |          |
Then the basket has 1 items
And the item 'butter' has a quantity of 2

Scenario: Adding an item with, or updating an item to a quantity of 0 or less will result in an ArgumentOutOfRangeException being thrown
When the following items are added:
| Id | Name | UnitPrice | Quantity | TaxRules |
| 1  | eggs |           | 0        |          |
Then A 'ArgumentOutOfRangeException' exception is thrown with message 'Item quantity cannot be less than 1'

Scenario: Updating an item to a quantity of 0 or less will result in an ArgumentOutOfRangeException being thrown
When the following items are added:
| Id | Name   | UnitPrice | Quantity | TaxRules |
| 1  | cerial |           | 1        |          |
| 1  | cerial |           | -1       |          |
Then A 'ArgumentOutOfRangeException' exception is thrown with message 'Item quantity cannot be less than 1'

# TOTALS
Scenario: After adding a single item to an empty basket, both the subtotal of the item and basket should equal the items unit price
When the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | pasta | £1.23     |          |          |
Then the basket contains the following items:
| Id | Name  | Quantity | SubTotal | Tax | Total |
| 1  | pasta | 1        | £1.23    | 0   | £1.23 |
And the basket has the following totals:
| SubTotal | Tax | Total |
| £1.23    | 0   | £1.23 |

Scenario: After adding a single item (with a NoTax rule) to an empty basket, both the tax of the item and the basket should equal 0 and both the subtotal of the item and basket should equal the items unit price
When the following items are added:
| Id | Name  | Quantity | UnitPrice | TaxRuleIds |
| 1  | pasta |          | £1.23     | 1          |
Then the basket contains the following items:
| Id | Name  | Quantity | SubTotal | Tax | Total |
| 1  | pasta | 1        | £1.23    | 0   | £1.23 |
And the basket has the following totals:
| SubTotal | Tax | Total |
| £1.23    | 0 | £1.23 |

Scenario: A percentage tax rule returns the correct totals for the basket and each item
When the following items are added:
| Id | Name | Quantity | UnitPrice | TaxRuleIds |
| 1  | XBox |          | £200      | 2          |
Then the basket contains the following items:
| Id | Name | Quantity | SubTotal | Tax | Total |
| 1  | XBox | 1        | £200     | £40 | £240  |
And the basket has the following totals:
| SubTotal | Tax | Total |
| £200    | £40 | £240 |

Scenario: A flat administration tax rule returns the correct totals for the basket and each item
When the following items are added:
| Id | Name | Quantity | UnitPrice | TaxRuleIds |
| 1  | XBox |          | £200      | 3          |
Then the basket contains the following items:
| Id | Name | Quantity | SubTotal | Tax | Total |
| 1  | XBox | 1        | £200     | £5  | £205  |
And the basket has the following totals:
| SubTotal | Tax | Total |
| £200    | £5 | £205 |

Scenario: A banded tax rule returns the correct totals for the basket and each item
When the following items are added:
| Id | Name | Quantity | UnitPrice | TaxRuleIds |
| 1  | XBox |          | £200      | 4          |
Then the basket contains the following items:
| Id | Name | Quantity | SubTotal | Tax | Total |
| 1  | XBox | 1        | £200     | £25 | £225  |
And the basket has the following totals:
| SubTotal | Tax | Total |
| £200    | £25 | £225 |

Scenario: An item with multiple tax rules returns the correct totals for the basket and each item
When the following items are added:
| Id | Name | Quantity | UnitPrice | TaxRuleIds |
| 1  | XBox |          | £200      | 2,3        |
Then the basket contains the following items:
| Id | Name | Quantity | SubTotal | Tax | Total |
| 1  | XBox | 1        | £200     | £45 | £245  |

Scenario: After adding two items with different quantities (both with a NoTax rule) to an empty basket, both the tax of the item and the basket should equal 0, both the subtotal and the total of the item and basket should equal the items unit price
When the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRuleIds |
| 1  | pasta | 50p       | 3        | 1          |
| 2  | rice  | £1        | 2        | 1          |
Then the basket contains the following items:
| Id | Name  | Quantity | SubTotal | Tax | Total | 
| 1  | pasta | 3        | £1.50    | 0   | £1.50 | 
| 2  | rice  | 2        | £2       | 0   | £2    | 
And the basket has the following totals:
| SubTotal | Tax | Total |
| £3.50    | 0   | £3.50 |

Scenario: After adding two items each with a different tax rule, totals should be correct for the basket and each item
When the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRuleIds |
| 1  | pasta | 50p       | 3        | 1          |
| 2  | rice  | £1        | 2        | 2          |
Then the basket contains the following items:
| Id | Name  | Quantity | SubTotal | Tax | Total |
| 1  | pasta | 3        | £1.50    | 0   | £1.50 |
| 2  | rice  | 2        | £2       | 40p | £2.40 |
And the basket has the following totals:
| SubTotal | Tax | Total |
| £3.50    | 40p | £3.90 |

# EVENTS
Scenario: Email notifications are sent correctly when items are added and removed
When the users subscribe as follows for the communication types:
| UserId | CommunicationTypes |
| 1      | email              |
And the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | pasta |           |          |          |
And items '1' are removed
Then only the following notifications are received
| UserId | UserType | CommunicationType | Publisher | UpdateType |
| 1      | Customer | email             | basket    | add        |
| 1      | Customer | email             | item      | add        |
| 1      | Customer | email             | basket    | remove     |
| 1      | Customer | email             | item      | remove     |

Scenario: Text notifications are sent correctly when items are added and removed
When the users subscribe as follows for the communication types:
| UserId | CommunicationTypes |
| 2      | text               |
And the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | pasta |           |          |          |
And items '1' are removed
Then only the following notifications are received
| UserId | UserType | CommunicationType | Publisher | UpdateType |
| 2      | Retailer | text              | basket    | add        |
| 2      | Retailer | text              | item      | add        |
| 2      | Retailer | text              | basket    | remove     |
| 2      | Retailer | text              | item      | remove     |

Scenario: No notifications are sent on the attempted removal of a non existant item
When the users subscribe as follows for the communication types:
| UserId | CommunicationTypes |
| 3      | email, text        |
And items '1' are removed
Then no notifications are received.

Scenario: No notifications are sent on the attempted addition using an invalid quantity
When the users subscribe as follows for the communication types:
| UserId | CommunicationTypes |
| 1      | email              |
And the following items are added:
| Id | Name   | UnitPrice | Quantity | TaxRules |
| 1  | eggs   |           | 0        |          |
| 2  | cerial |           | -1       |          |
Then no notifications are received.

Scenario: All notifications are sent correctly for a variety of users and subscriptions
When the users subscribe as follows for the communication types:
| UserId | CommunicationTypes |
| 1      | email, text        |
| 2      | email, text        |
| 3      | email              |
| 4      | text               |
| 5      |                    |
When the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | pasta |           |          |          |
Then only the following notifications are received
| UserId | UserType | CommunicationType | Publisher | UpdateType |
| 1      | Customer | email             | basket    | add        |
| 1      | Customer | email             | item      | add        |
| 2      | Retailer | email             | basket    | add        |
| 2      | Retailer | email             | item      | add        |
| 3      | Customer | email             | basket    | add        |
| 3      | Customer | email             | basket    | add        |
| 1      | Customer | text              | basket    | add        |
| 1      | Customer | text              | item      | add        |
| 2      | Retailer | text              | basket    | add        |
| 2      | Retailer | text              | item      | add        |
| 4      | Retailer | text              | basket    | add        |
| 4      | Retailer | text              | basket    | add        |












