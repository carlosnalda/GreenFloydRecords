using CarlosNalda.GreenFloydRecords.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarlosNalda.GreenFloydRecords.WebApp.DatabaseInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;

        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            SeedGenres();
            SeedArtists();
            SeedVinylrecords();
        }

        private void SeedGenres()
        {
            _db.Genre.Add(
                new Genre
                {
                    Id = Guid.Parse(IdConstants.GenreGrungeId),
                    Name = "Grunge",
                });
            _db.Genre.Add(
                new Genre
                {
                    Id = Guid.Parse(IdConstants.GenreAlternativeRockId),
                    Name = "Alternative Rock",
                });
            _db.Genre.Add(
                new Genre
                {
                    Id = Guid.Parse(IdConstants.GenreProgressiveRockId),
                    Name = "Progressive Rock",
                });
            _db.Genre.Add(
                new Genre
                {
                    Id = Guid.Parse(IdConstants.GenreArtRockId),
                    Name = "Art Rock",
                });
            _db.Genre.Add(
                new Genre
                {
                    Id = Guid.Parse(IdConstants.GenrePostPunkId),
                    Name = "Post-Punk",
                });
            _db.Genre.Add(
                new Genre
                {
                    Id = Guid.Parse(IdConstants.GenreRockUrbanoEspanolId),
                    Name = "Rock urbano espanol",
                });
            _db.SaveChanges();
        }

        private void SeedArtists()
        {
            if (!_db.Set<Artist>().Any())
            {
                _db.Artist.Add(
                    new Artist
                    {
                        Id = Guid.Parse(IdConstants.ArtistNirvanaId),
                        Name = "Nirvana",
                        Formed = new DateTime(1987, 01, 01),
                        Disbanded = new DateTime(1994, 04, 08),
                    });
                _db.Artist.Add(
                    new Artist
                    {
                        Id = Guid.Parse(IdConstants.ArtistRadioheadId),
                        Name = "Radiohead",
                        Formed = new DateTime(1991, 01, 01),
                    });
                _db.Artist.Add(
                    new Artist
                    {
                        Id = Guid.Parse(IdConstants.ArtistTheMarsVoltaId),
                        Name = "The Mars Volta",
                        Formed = new DateTime(2001, 01, 01),
                    });
                _db.Artist.Add(
                    new Artist
                    {
                        Id = Guid.Parse(IdConstants.ArtistPinkFloydId),
                        Name = "Pink Floyd",
                        Formed = new DateTime(1963, 01, 01),
                    });
                _db.Artist.Add(
                    new Artist
                    {
                        Id = Guid.Parse(IdConstants.ArtistKingCrimsonId),
                        Name = "King Crimson",
                        Formed = new DateTime(1969, 01, 01),
                        Disbanded = new DateTime(2021, 01, 01),
                    });
                _db.Artist.Add(
                    new Artist
                    {
                        Id = Guid.Parse(IdConstants.ArtistJoyDivisionId),
                        Name = "Joy Division",
                        Formed = new DateTime(1977, 01, 01),
                        Disbanded = new DateTime(1980, 01, 01),
                    });
                _db.Artist.Add(
                    new Artist
                    {
                        Id = Guid.Parse(IdConstants.ArtistExtremoduroId),
                        Name = "Extremoduro",
                        Formed = new DateTime(1987, 01, 01),
                        Disbanded = new DateTime(2019, 01, 01),
                    });

                _db.SaveChanges();
            }
        }

        private void SeedVinylrecords()
        {
            if (!_db.Set<VinylRecord>().Any())
            {
                _db.VinylRecord.Add(
                    new VinylRecord
                    {
                        Id = Guid.Parse(IdConstants.VinylRecordInUteroId),
                        Name = "In Utero",
                        Description = "In Utero is the third and final studio album by the American rock band Nirvana. It was released on September 21, 1993, by DGC Records. After breaking into the mainstream with their second album, Nevermind (1991), Nirvana hired Steve Albini to record In Utero, seeking a more complex, abrasive sound that was also reminiscent of their debut album, Bleach (1989). Although frontman and primary songwriter Kurt Cobain claimed that the album was \"very impersonal\", many of its songs contain heavy allusions to his personal life and struggles, expressing feelings of angst that were common on Nevermind. ",
                        ReleaseDate = new DateTime(1993, 09, 21),
                        Rate = 4.03m,
                        Price = 44.5m,
                        GenreId = Guid.Parse(IdConstants.GenreGrungeId),
                        ArtistId = Guid.Parse(IdConstants.ArtistNirvanaId),
                        ImageUrl = IdConstants.ImageInUteroId
                    });
                _db.VinylRecord.Add(
                   new VinylRecord
                   {
                       Id = Guid.Parse(IdConstants.VinylRecordOkComputerId),
                       Name = "OK Computer",
                       Description = "OK Computer is the third studio album by the English rock band Radiohead, released in the UK on 16 June 1997 by EMI. OK Computer's abstract lyrics, densely layered sound and eclectic influences laid the groundwork for Radiohead's later, more experimental work. The album's lyrics depict a world fraught with rampant consumerism, social alienation, emotional isolation and political malaise; in this capacity, OK Computer is said to have prescient insight into the mood of 21st-century life.",
                       ReleaseDate = new DateTime(1997, 06, 16),
                       Rate = 4.27m,
                       Price = 55.75m,
                       GenreId = Guid.Parse(IdConstants.GenreAlternativeRockId),
                       ArtistId = Guid.Parse(IdConstants.ArtistRadioheadId),
                       ImageUrl = IdConstants.ImageOkComputerId
                   });
                _db.VinylRecord.Add(
                   new VinylRecord
                   {
                       Id = Guid.Parse(IdConstants.VinylRecordFrancesTheMuteId),
                       Name = "Frances the Mute",
                       Description = "Frances the Mute is the second studio album by American progressive rock band The Mars Volta released in February 2005 on Gold Standard Laboratories and Universal. Produced by guitarist and songwriter Omar Rodriguez-Lopez, the album incorporates dub, ambient, Latin and jazz influences. Frances the Mute is comparable to The Mars Volta's 2003 release De-Loused in the Comatorium, with its cryptic lyrics and highly layered instrumentals.",
                       ReleaseDate = new DateTime(2005, 02, 01),
                       Rate = 3.87m,
                       Price = 73m,
                       GenreId = Guid.Parse(IdConstants.GenreProgressiveRockId),
                       ArtistId = Guid.Parse(IdConstants.ArtistTheMarsVoltaId),
                       ImageUrl = IdConstants.ImageFrancesTheMuteId
                   });
                _db.VinylRecord.Add(
                   new VinylRecord
                   {
                       Id = Guid.Parse(IdConstants.VinylRecordTheDarkSideOfTheMoonId),
                       Name = "The Dark Side of the Moon",
                       Description = "The Dark Side of the Moon is the eighth studio album by the English rock band Pink Floyd, released on 1 March 1973 by Harvest Records in the UK and Capitol Records in the US. it was conceived as a concept album that would focus on the pressures faced by the band during their arduous lifestyle, and also deal with the mental health problems of former band member Syd Barrett. The record builds on ideas explored in Pink Floyd's earlier recordings and performances, while omitting the extended instrumentals that characterised the band's earlier work. The group employed multitrack recording, tape loops, and analogue synthesisers, including experimentation with the EMS VCS 3 and a Synthi A. The Dark Side of the Moon centers on the idea of madness, exploring themes such as conflict, greed, time, death, and mental illness. Snippets from interviews with the band's road crew and others are featured alongside philosophical quotations.",
                       ReleaseDate = new DateTime(1973, 03, 01),
                       Rate = 4.23m,
                       Price = 61.5m,
                       GenreId = Guid.Parse(IdConstants.GenreArtRockId),
                       ArtistId = Guid.Parse(IdConstants.ArtistPinkFloydId),
                       ImageUrl = IdConstants.ImageTheDarkSideOfTheMoonId
                   });
                _db.VinylRecord.Add(
                   new VinylRecord
                   {
                       Id = Guid.Parse(IdConstants.VinylRecordWishYouWereHereId),
                       Name = "Wish You Were Here",
                       Description = "Wish You Were Here is the ninth studio album by the English rock band Pink Floyd, released on 12 September 1975. The themes include alienation and criticism of the music business. The bulk of the album is taken up by \"Shine On You Crazy Diamond\", a nine-part tribute to founding member Syd Barrett, who left the band seven years earlier due to his deteriorating mental health. Barrett coincidentally visited during the album's production in 1975. Like their previous record, The Dark Side of the Moon (1973), Pink Floyd used studio effects and synthesisers.",
                       ReleaseDate = new DateTime(1975, 09, 12),
                       Rate = 4.33m,
                       Price = 39m,
                       GenreId = Guid.Parse(IdConstants.GenreProgressiveRockId),
                       ArtistId = Guid.Parse(IdConstants.ArtistPinkFloydId),
                       ImageUrl = IdConstants.ImageWishYouWereHereId
                   });
                _db.VinylRecord.Add(
                   new VinylRecord
                   {
                       Id = Guid.Parse(IdConstants.VinylRecordLarksTonguesInAspicId),
                       Name = "Larks' Tongues in Aspic",
                       Description = "Larks' Tongues in Aspic is the fifth studio album by the English progressive rock group King Crimson, released on 23 March 1973. This album is the debut of King Crimson's third incarnation, featuring co-founder and guitarist Robert Fripp along with four new members: bass guitarist and vocalist John Wetton, violinist and keyboardist David Cross, percussionist Jamie Muir, and drummer Bill Bruford. It is a key album in the band's evolution, drawing on Eastern European classical music and European free improvisation as central influences.",
                       ReleaseDate = new DateTime(1973, 03, 01),
                       Rate = 4.03m,
                       Price = 83m,
                       GenreId = Guid.Parse(IdConstants.GenreProgressiveRockId),
                       ArtistId = Guid.Parse(IdConstants.ArtistKingCrimsonId),
                       ImageUrl = IdConstants.ImageLarksTonguesInAspicId
                   });
                _db.VinylRecord.Add(
                   new VinylRecord
                   {
                       Id = Guid.Parse(IdConstants.VinylUnknownPleasuresId),
                       Name = "Unknown Pleasures",
                       Description = "Unknown Pleasures is the debut studio album by the English rock band Joy Division, released on 15 June 1979 by Factory Records.[2] The album was recorded and mixed over three successive weekends at Stockport's Strawberry Studios in April 1979, with producer Martin Hannett contributing a number of unconventional recording techniques to the group's sound. The cover artwork was designed by artist Peter Saville, using a data plot of signals from a radio pulsar.[3] It is the only Joy Division album released during lead singer Ian Curtis's lifetime. ",
                       ReleaseDate = new DateTime(1979, 06, 15),
                       Rate = 4.1m,
                       Price = 31m,
                       GenreId = Guid.Parse(IdConstants.GenrePostPunkId),
                       ArtistId = Guid.Parse(IdConstants.ArtistJoyDivisionId),
                       ImageUrl = IdConstants.ImageUnknownPleasuresId
                   });
                _db.VinylRecord.Add(
                   new VinylRecord
                   {
                       Id = Guid.Parse(IdConstants.VinylRecordLaLeyInnataId),
                       Name = "La Ley Innata",
                       Description = "La ley innata es el noveno álbum de estudio de la banda de rock española Extremoduro, producido por Iñaki “Uoho” Antón y publicado por Warner Music el 9 de septiembre de 2008.3​ Se trata de un álbum conceptual compuesto de una sola canción de 45 minutos, a su vez dividida en seis partes diferenciadas por pistas independientes. Fue el primer álbum de la banda en llegar a lo más alto de las listas de ventas españolas.4​ Tras una semana en el número uno en ventas nacionales se mantuvo otras dos en el segundo puesto. El álbum acusó un cambio en el porvenir de las composiciones del grupo. La complejidad de su estructura y el uso de un gran número de músicos adicionales, entre ellos el violinista Ara Malikian, supuso un punto de inflexión en el estilo de la banda. 5",
                       ReleaseDate = new DateTime(2008, 09, 09),
                       Rate = 3.81m,
                       Price = 42m,
                       GenreId = Guid.Parse(IdConstants.GenreRockUrbanoEspanolId),
                       ArtistId = Guid.Parse(IdConstants.ArtistExtremoduroId),
                       ImageUrl = IdConstants.ImageLaLeyInnataId
                   });

                _db.SaveChanges();
            }
        }
    }
}
