Feature: ShoppingBasket
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

# FIGURE OUT WHAT TAGS ARE IN SPECFLOW
#@mytag
#Scenario: Add two numbers
#	Given I have entered 50 into the calculator
#	And I have entered 70 into the calculator
#	When I press add
#	Then the result should be 120 on the screen

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
| Id | NotificationSystemName | CommunicationType | CommunicationChannel |
| 1  | EmailNotification      | email             | ..\..\..\..Emails       |
| 2  | TextNotification       | text              | ..\..\..\..Phones       |
And the following users
| Id | UserType | Email                | PhoneNumber |
| 1  | Customer | albert@hotmail.co.uk | 23855638    |
| 2  | Retailer | belle@yahoo.co.uk    | 86120183    |
| 3  | Customer | charlie@gmail.co.uk  | 41843576    |
| 4  | Retailer | diana@outlook.co.uk  | 03110894    |
| 5  | Customer | edward@aol.co.uk     | 84315325    |
And all notification systems subscribe to the basket

## INITIALISATION
#########################################################################################################################
Scenario: A newly constructed basket has zero items
Then the basket has 0 items

Scenario: The totals of a newly constructed basket are all zero
Then all totals are 0

## QUANTITIES	!!!!!!!!!!!!!!!!!!!!! NEED TESTS FOR REMOVING !!!!!!!!!!!!!!!!!!!!!
#########################################################################################################################

# test unavailable item

# tableA
Scenario: Adding an item without an explicit quantity results in a quantity of 1 for the item
When the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | pasta |           |          |          |
Then the basket has 1 items
And the item 'pasta' has a quantity of 1

# tableA
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

# table for lower, higher, same
Scenario: After adding a single item to the basket, adding the same item again will update the quantity of the previously added item
When the following items are added:
| Id | Name   | UnitPrice | Quantity | TaxRules |
| 1  | butter |           | 4        |          |
| 1  | butter |           | 2        |          |
Then the basket has 1 items
And the item 'butter' has a quantity of 2

# Repetition of the above?
#Scenario: After updating the quantity on an item already in a basket, both the basket and item quantities are correct

# tableA ( for 0 and a negative number)
Scenario: Adding an item with, or updating an item to a quantity of 0 or less will result in an ArgumentOutOfRangeException being thrown
When the following items are added:
| Id | Name | UnitPrice | Quantity | TaxRules |
| 1  | eggs |           | 0        |          |
Then A 'ArgumentOutOfRangeException' exception is thrown with message 'Item quantity cannot be less than 1'

# tableA ( for 0 and a negative number)
Scenario: Updating an item to a quantity of 0 or less will result in an ArgumentOutOfRangeException being thrown
When the following items are added:
| Id | Name   | UnitPrice | Quantity | TaxRules |
| 1  | cerial |           | 1        |          |
| 1  | cerial |           | -1       |          |
Then A 'ArgumentOutOfRangeException' exception is thrown with message 'Item quantity cannot be less than 1'

## TOTALS
#########################################################################################################################
Scenario: After adding a single item to an empty basket, both the subtotal of the item and basket should equal the items unit price
When the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | pasta | £1.23     |          |          |
Then the item 'pasta' has a subtotal of '£1.23'
#And the basket has a subtotal of '50p'
# Awaiting functionality to update basket subtotal

# Remove to own feature and test for multiple quantity, throwing exceptions when not found
Scenario: After adding a single item (with a NoTax rule) to an empty basket, both the tax of the item and the basket should equal 0 and both the subtotal of the item and basket should equal the items unit price
When the following items are added:
| Id | Name  | Quantity | UnitPrice | TaxRuleIds |
| 1  | pasta |          | £1.23     | 1          |
Then the basket contains the following items:
| Id | Name  | Quantity | SubTotal | Tax | Total |
| 1  | pasta | 1        | £1.23    | 0   | £1.23 |

Scenario: A percentage tax rule returns the correct totals for the basket and each item
When the following items are added:
| Id | Name | Quantity | UnitPrice | TaxRuleIds |
| 1  | XBox |          | £200      | 2          |
Then the basket contains the following items:
| Id | Name | Quantity | SubTotal | Tax | Total |
| 1  | XBox | 1        | £200     | £40 | £240  |

Scenario: A flat administration tax rule returns the correct totals for the basket and each item
When the following items are added:
| Id | Name | Quantity | UnitPrice | TaxRuleIds |
| 1  | XBox |          | £200      | 3          |
Then the basket contains the following items:
| Id | Name | Quantity | SubTotal | Tax | Total |
| 1  | XBox | 1        | £200     | £5  | £205  |

Scenario: A banded tax rule returns the correct totals for the basket and each item
When the following items are added:
| Id | Name | Quantity | UnitPrice | TaxRuleIds |
| 1  | XBox |          | £200      | 4          |
Then the basket contains the following items:
| Id | Name | Quantity | SubTotal | Tax | Total |
| 1  | XBox | 1        | £200     | £25 | £225  |

Scenario: An item with multiple tax rules returns the correct totals for the basket and each item
When the following items are added:
| Id | Name | Quantity | UnitPrice | TaxRuleIds |
| 1  | XBox |          | £200      | 2,3        |
Then the basket contains the following items:
| Id | Name | Quantity | SubTotal | Tax | Total |
| 1  | XBox | 1        | £200     | £45 | £245  |

Scenario: After adding two items with different quantities (both with a NoTax rule) to an empty basket, both the tax of the item and the basket should equal 0, both the subtotal and the total of the item and basket should equal the items unit price
When the following items are added:
| Name  | UnitPrice | Quantity | TaxRuleIds |
| pasta | 50p       | 3        | 1          |
| rice  | £1        | 2        | 1          |
Then the basket contains the following items:
| Name  | Quantity | SubTotal | Tax | Total | 
| pasta | 3        | £1.50    | 0   | £1.50 | 
| rice  | 2        | £2       | 0   | £2    | 
And the basket has the following totals:
| SubTotal | Tax | Total |
| £3.50    | 0   | £3.50 |

Scenario: After adding two items each with a different tax rule, totals should be correct for the basket and each item
When the following items are added:
| Name  | UnitPrice | Quantity | TaxRuleIds |
| pasta | 50p       | 3        | 1          |
| rice  | £1        | 2        | 2          |
Then the basket contains the following items:
| Name  | Quantity | SubTotal | Tax | Total |
| pasta | 3        | £1.50    | 0   | £1.50 |
| rice  | 2        | £2       | 40p | £2.40 |
And the basket has the following totals:
| SubTotal | Tax | Total |
| £3.50    | 40p | £3.90 |

## EVENTS
#########################################################################################################################













