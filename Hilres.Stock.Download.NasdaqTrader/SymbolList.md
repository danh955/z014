# Symbol list file definition

### NasdaqTrader refinance:
- [Symbol Lookup](http://www.nasdaqtrader.com/Trader.aspx?id=symbollookup)
- [Symbol Look-Up/Directory Data Fields & Definitions](http://www.nasdaqtrader.com/trader.aspx?id=symboldirdefs)

---

## NASDAQ symbol list CSF file

#### Symbol (0)

Gets the one to four or five character identifier for each NASDAQ-listed security.

#### SecurityName (1)

Gets company issuing the security.

#### MarketCategory (2)

Gets the category assigned to the issue by NASDAQ based on Listing Requirements.

Values:

- Q = NASDAQ Global Select MarketSM
- G = NASDAQ Global MarketSM
- S = NASDAQ Capital Market.

#### TestIssue (3)

Gets a value indicating whether or not the security is a test security.

Values:

- Y = yes, it is a test issue.
- N = no, it is not a test issue.

#### FinancialStatus (4)

Gets indicates when an issuer has failed to submit its regulatory filings on a timely basis, has failed
to meet NASDAQ's continuing listing standards, and/or has filed for bankruptcy.

Values include:

- D = Deficient: Issuer Failed to Meet NASDAQ Continued Listing Requirements
- E = Delinquent: Issuer Missed Regulatory Filing Deadline
- Q = Bankrupt: Issuer Has Filed for Bankruptcy
- N = Normal(Default): Issuer Is NOT Deficient, Delinquent, or Bankrupt.
- G = Deficient and Bankrupt
- H = Deficient and Delinquent
- J = Delinquent and Bankrupt
- K = Deficient, Delinquent, and Bankrupt.

#### RoundLotSize (5)

Gets the number of shares that make up a round lot for the given security.

#### ETF (6)

Gets a value indicating whether the security is an exchange traded fund (ETF).
Possible values:

- Y = Yes, security is an ETF
- N = No, security is not an ETF

For new ETFs added to the file, the ETF field for the record will be updated to a value of "Y".

#### NextShares (7)

Gets a value indicating whether its next shares.

---

## Other symbol list CSF file

#### ActSymbol (0)

Gets identifier for each security used in ACT and CTCI connectivity protocol. Typical identifiers
have 1-5 character root symbol and then 1-3 characters for suffixes. Allow up to 14 characters.

#### SecurityName (1)

Gets the name of the security including additional information, if applicable. Examples are security
type (common stock, preferred stock, etc.) or class (class A or B, etc.). Allow up to 255 characters.

#### Exchange (2)

Gets the listing stock exchange or market of a security.

Allowed values are:

- A = NYSE MKT
- N = New York Stock Exchange(NYSE)
- P = NYSE ARCA
- Z = BATS Global Markets(BATS)
- V = Investors' Exchange, LLC (IEXG).

#### CqsSymbol (3)

Gets identifier of the security used to disseminate data via the SIAC Consolidated Quotation System (CQS) and
Consolidated Tape System (CTS) data feeds. Typical identifiers have 1-5 character root symbol and then
1-3 characters for suffixes. Allow up to 14 characters.

#### ETF (4)

Gets a value indicating whether the security is an exchange traded fund (ETF).

Possible values:

- Y = Yes, security is an ETF
- N = No, security is not an ETF

For new ETFs added to the file, the ETF field for the record will be updated to a value of "Y".

#### RoundLotSize (5)

Gets indicates the number of shares that make up a round lot for the given security. Allow up to 6 digits.

#### TestIssue (6)

Gets a value indicating whether the security is a test security.

- Y = Yes, it is a test issue.
- N = No, it is not a test issue.

#### NASDAQSymbol (7)

Gets identifier of the security used to in various NASDAQ connectivity protocols and NASDAQ market data feeds. Typical
identifiers have 1-5 character root symbol and then 1-3 characters for suffixes. Allow up to 14 characters.

---

### File Creation Time:

The last row of each Symbol Directory text file contains a time-stamp that reports the File Creation Time.
The file creation time is based on when NASDAQ Trader generates the file and can be used to determine the
timeliness of the associated data. The row contains the words File Creation Time followed by mmddyyyyhhmm
as the first field, followed by all delimiters to round out the row.

An example: File Creation Time: 1217200717:03|||||
