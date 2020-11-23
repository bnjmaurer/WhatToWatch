using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using WhatToWatch.Api.Models;
using System.Text.Json;
using WhatToWatch.Api.ExternalModels;

namespace WhatToWatch.Api.Services
{
    public class MovieDbApiRepository : IMovieInfoRepository
    {
        private readonly IConfiguration configuration;

        public MovieDbApiRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<MovieInfo> FindMovieAsync(string movieName)
        {
            if (String.IsNullOrWhiteSpace(movieName))
                throw new ArgumentNullException(nameof(movieName));

            var movieInfo = new MovieInfo();
            using (var httpClient = new HttpClient())
            {
                string apiKey = configuration.GetValue<string>("MovieDbApiKey");
                string movieNameCorrected = movieName.Trim().Replace(' ', '+');
                string uri = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={movieNameCorrected}";
                var response = await httpClient.GetAsync(uri);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var content = await response.Content.ReadAsStringAsync();
                        var movieDbApiSearchMovie = JsonSerializer.Deserialize<MovieDbApiSearchMovie>(content);

                        if (movieDbApiSearchMovie.results.Count == 0)
                            return null;

                        movieInfo.MovieDbApiId = movieDbApiSearchMovie.results[0].id;
                        movieInfo.Name = movieDbApiSearchMovie.results[0].title;
                        var releaseDate = DateTime.Parse(movieDbApiSearchMovie.results[0].release_date);
                        movieInfo.Year = releaseDate.Year;
                        break;
                    case HttpStatusCode.Unauthorized:
                        throw new AuthenticationException("Movie Db Api key is incorrect");
                    case HttpStatusCode.NotFound:
                        break;
                }                    
            }
            return movieInfo;
        }

        public async Task GetMovieDetailsAsync(MovieInfo movieInfo)
        {
            if (movieInfo.MovieDbApiId == 0)
                throw new ArgumentNullException(nameof(movieInfo), "Id manquant");

            using (var httpClient = new HttpClient())
            {
                string apiKey = configuration.GetValue<string>("MovieDbApiKey");
                string externalIdsUri = $"https://api.themoviedb.org/3/movie/{movieInfo.MovieDbApiId}/external_ids?api_key={apiKey}";
                var externalIdsResponse = await httpClient.GetAsync(externalIdsUri);
                switch (externalIdsResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var externalIdsContent = await externalIdsResponse.Content.ReadAsStringAsync();
                        var movieDbApiMoviesExternalIds = JsonSerializer.Deserialize<MovieDbApiMoviesExternalIds>(externalIdsContent);
                        movieInfo.IMDBId = movieDbApiMoviesExternalIds.imdb_id;
                        break;
                    case HttpStatusCode.Unauthorized:
                        throw new AuthenticationException("Movie Db Api key is incorrect");
                    case HttpStatusCode.NotFound:
                        return;
                }

                string uri = $"https://api.themoviedb.org/3/movie/{movieInfo.MovieDbApiId}?api_key={apiKey}";
                var response = await httpClient.GetAsync(uri);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var content = await response.Content.ReadAsStringAsync();
                        var movieDbApiMoviesDetails = JsonSerializer.Deserialize<MovieDbApiMoviesDetails>(content);                        
                        movieInfo.DurationInMinutes = movieDbApiMoviesDetails.runtime;
                        break;
                    case HttpStatusCode.Unauthorized:
                        throw new AuthenticationException("Movie Db Api key is incorrect");
                    case HttpStatusCode.NotFound:
                        break;
                }
            }
        }
    }
}
