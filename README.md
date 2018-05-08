The **BI/public** branch in this fork of this code is managed by [Business Integrations Ltd](https://github.com/BusinessIntegrations).

A changelog is appended at the end of this file. Further information on our coding standards and approach are detailed [here](https://businessintegrations.github.io/).

***

## Business Integrations Changelog
1. Module rework 2018-04-04
   * Fixed reliance on deprecated jQuery module.
   * Removed redundant import/export code.
   * Added an extra route (googlesitemap.xml) for compatibility with one of our previously developed sites.
   * Deal with bug when BaseUrl is empty - use request URL instead.
   * Any AutoRoute part that is defined as the HomePage is included in the sitemap as the root of the site instead of its original path. (i.e. ~/ instead of ~/HomePage)
   * Fixed bug in HomeController where empty enumerable caused an exception.
   * Fixed empty columns to display correctly.
   * Code tidy
   * Update readme.md
2. Code refactor 2018-05-04
   * Renamed a number of methods and classes to apply come naming consistency as seemed a little muddled.
   * Rationalised string constants, updated module version, updated placement info, general code tidy etc.
