Sentence Compression - compression for very small strings
-------------------

This is a port of <a href="https://github.com/antirez/smaz">SMAZ c-library</a> a simple compression library for compressing short
tweet-like strings.

Although the code differs a lot, coming from c to c#, Sentence Compression should be 
100% compatible with the original library.

I ported it to test it against and in combination with other libraries to be able to 
store short strings individually for fast read/write access.

It really can't compress anything else but plain strings, but on most plain English texts
it will archive around 40-50% compression ratio. 

It also handles HTML, urls etc. and can even archive good results on really short strings 
of two or three bytes.

Most other libraries won't compress text shorter than 100 bytes

These are some compression examples from the original author:

Compression examples
--------------------

* 'This is a small string' compressed by 50%
* 'foobar' compressed by 34%
* 'the end' compressed by 58%
* 'not-a-g00d-Exampl333' enlarged by 15%
* 'Smaz is a simple compression library' compressed by 39%
* 'Nothing is more difficult, and therefore more precious, than to be able to decide' compressed by 49%
* 'this is an example of what works very well with smaz' compressed by 49%
* '1000 numbers 2000 will 10 20 30 compress very little' compressed by 10%

In general, lowercase English will work very well. It will suck with a lot
of numbers inside the strings. Other languages are compressed pretty well too,
the following is Italian, not very similar to English but still compressible
by smaz:

* 'Nel mezzo del cammin di nostra vita, mi ritrovai in una selva oscura' compressed by 33%

It can compress URLS pretty well:

* 'http://google.com' compressed by 59%
* 'http://programming.reddit.com' compressed by 52%
* 'http://github.com/antirez/smaz/tree/master' compressed by 46%

Usage
-----

The lib consists of just two functions:

	string Compress(string input);

Compress the string input and return the compressed data. 

    string Decompress(string input);

Decompress the string input and return the decompressed data.


Credits
-------

Sentence Compression was written by Poul Foged, based on the work of Salvatore Sanfilippo and is released under the BSD license. Check the COPYING file for more information.