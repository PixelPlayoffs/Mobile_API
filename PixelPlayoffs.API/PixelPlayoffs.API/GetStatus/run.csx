#r "Newtonsoft.Json"

using System.Net;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    // Get request body and check passcode - return 401 in invalid
    Passcode data = await req.Content.ReadAsAsync<Passcode>();
    if (data.Code != "fatmamba")
    {
        return req.CreateResponse(HttpStatusCode.Unauthorized, "Incorrect token passed to API");
    }

    Battle battle = new Battle();
    battle.GetCurrentBattle();

    var battleData = JsonConvert.SerializeObject(battle);

    return req.CreateResponse(HttpStatusCode.OK, battleData);
}

public class Artist
{
    public string ArtistId { get; set; }
    public string ArtistName { get; set; }
    public int Score { get; set; }
    public string BattleId { get; set; }
}

public class Battle
{
    public string BattleId { get; set; }
    public string BattleName { get; set; }
    public string BattleStatus { get; set; }
    public TimeSpan TimeLeft { get; set; }
    public Artist ArtistOne { get; set; }
    public Artist ArtistTwo { get; set; }

    public void GetCurrentBattle()
    {
        this.BattleId = "0";
        this.BattleName = "Round One";
        this.BattleStatus = "Running";
        this.TimeLeft = new TimeSpan(0, 0, 803);
        this.ArtistOne = new Artist { ArtistId = "0", ArtistName = "Ian Philpot", BattleId = "0", Score = 98 };
        this.ArtistTwo = new Artist { ArtistId = "1", ArtistName = "Bree Philpot", BattleId = "0", Score = 102 };
    }
}

public class Passcode
{
    public string Code { get; set; }
}

public class Vote
{
    public string UserId { get; set; }
    public string ArtistId { get; set; }
    public Passcode Passcode { get; set; }
}