using System.Linq;
using NUnit.Framework;

namespace Devchamp.SentenceCompression.Tests
{
    [TestFixture]
    public class SentenceCompressorTests
    {
        [Test]
        public void Can_archive_ratio_on_small_string()
        {
            //Arrange
            var source = "This is a small string";
            
            //Act
            var result = SentenceCompressor.Compress(source);

            //Assert
            var compressionLevel = 100 - ((100 * result.Length) / source.Length);

            Assert.That(compressionLevel, Is.EqualTo(50));
        }

        [Test]
        public void Can_archive_ration_on_foobar()
        {
            //Arrange
            var source = "foobar";

            //Act
            var result = SentenceCompressor.Compress(source);

            //Assert
            var compressionLevel = 100 - ((100 * result.Length) / source.Length);

            Assert.That(compressionLevel, Is.EqualTo(34));
        }

        [Test]
        public void Can_archive_ration_on_italian()
        {
            //Arrange
            var source = "Nel mezzo del cammin di nostra vita, mi ritrovai in una selva oscura";

            //Act
            var result = SentenceCompressor.Compress(source);

            //Assert
            var compressionLevel = 100 - ((100 * result.Length) / source.Length);

            Assert.That(compressionLevel, Is.EqualTo(33));
        }

        [Test]
        public void Can_archive_ration_on_url()
        {
            //Arrange
            var source = "http://programming.reddit.com";

            //Act
            var result = SentenceCompressor.Compress(source);

            //Assert
            var compressionLevel = 100 - ((100 * result.Length) / source.Length);

            Assert.That(compressionLevel, Is.EqualTo(52));
        }

        [Test]
        public void Can_handle_long_verbatim_string()
        {
            //Arrange
            var source = string.Join("", Enumerable.Range(0, 1000).Select(p => "æ").ToArray());

            var compressed = SentenceCompressor.Compress(source);

            //Act
            var result = SentenceCompressor.Decompress(compressed);

            //Assert
            Assert.That(source, Is.EqualTo(result));
        }
        
        [Test]
        public void Can_compress_mixed_text()
        {
            //Arrange
            var source = @"O Captain! My Captain! by Walt Whitman
                           --------------------------------------

                           1)
                           O CAPTAIN! my Captain! our fearful trip is done; 
                           The ship has weather'd every rack, the prize we sought is won; 
                           The port is near, the bells I hear, the people all exulting, 
                           While follow eyes the steady keel, the vessel grim and daring: 
                           But O heart! heart! heart!
                           O the bleeding drops of red, 
                           Where on the deck my Captain lies, 
                           Fallen cold and dead. 

                           2)
                           O Captain! my Captain! rise up and hear the bells; 
                           Rise up-for you the flag is flung-for you the bugle trills;
                           For you bouquets and ribbon'd wreaths-for you the shores a-crowding; 
                           For you they call, the swaying mass, their eager faces turning; 
                           Here Captain! dear father! 
                           This arm beneath your head; 
                           It is some dream that on the deck,
                           You've fallen cold and dead. 

                           3)
                           My Captain does not answer, his lips are pale and still; 
                           My father does not feel my arm, he has no pulse nor will; 
                           The ship is anchor'd safe and sound, its voyage closed and done; 
                           From fearful trip, the victor ship, comes in with object won;
                           Exult, O shores, and ring, O bells! 
                           But I, with mournful tread, 
                           Walk the deck my Captain lies, 
                           Fallen cold and dead.";
            
            var compressed = SentenceCompressor.Compress(source);

            //Act
            var result = SentenceCompressor.Decompress(compressed);

            //Assert
            Assert.That(result, Is.EqualTo(source));
        }
    }
}
