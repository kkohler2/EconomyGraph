# EconomyGraph
Xamarin graphing library intended for graphing of economic data.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 
[![Open Issues](https://img.shields.io/github/issues-raw/kkohler2/EconomyGraph.svg)](https://github.com/kkohler2/EconomyGraph/issues)

[Documentation](Documentation/documentation.md)

It was never my intention to create a graphing library.  For a very long time, I have been thinking about having a way to analyze data related to the United States economy - unemployment claims, gdp, housing starts, interest rates, etc.  The ultimate goal is to try to determine when the economy is about to turn south in an effort to know when to exit any financial investments.  In the year 2020, none of this would have been helpful.  The time to bail out was mid-February when there were indications that world-wide there were issues.  The time to get back in was about a month later, when the stock market dropped 10% in a day.  So I may not have succeeded in the original goal for this economic cycle, but the idea still persists.

I intended to either create a website or app that would graph the data, acquiring the relevant data from hopefully reliable sources, graph it and be able to analyze the data.  Somewhere along the line I found a video on youtube about how to use SkiaSharp and realized that creating a graphing library isn't that difficult.  There are a few basic graph drawing concepts that need to be done - draw lines, circles, rectangles, text and calculate test size.  If you can do that, the rest is basically just some data structures and calculations to put it together.

Although my intention is graphing economic data, this library is not limited in what kind of data can be graphed.  I have looked at dozens of examples from websites, books and newspapers as examples.  One example is the Horizontal Bar Graph.  This was designed specifically to replicate graphs found on the Washington Post website.

Shaded bar and line graphs are just bar and line graphs with the shaded areas inteneded to represent United States recessions.  This is a key feature of why I wrote the library.  I have seen graphs with this shading and even found a website with an interactive graph of housing starts with recession shading, but have not found an actual library that has such shading as a feature.  The other primary idea that I wanted was displaying data related x-axis labels that represent a range of data points, such as a year to indicate all data above the label is for that year.  Most graphing libraries I have tried have a label for every data point.  This just doesn't work when graphing something like multiple years worth of initial unemployment claims, which gives 52 data points per year.

At the time of this writing, documentation is not written, but there is a working test app that demonstrates all included features.  It is a Xamarin Forms app with projects for UWP, Android and iOS.  Most testing was done with UWP for simplicity.  Android has been tested, iOS has not.
