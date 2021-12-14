<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TotallyNotGuFundMe._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <style>
        .image-banner {
            position: relative;
            text-align: center;
        }
        .image-banner img {
            opacity: .20;
        }
        .image-banner h1 {
            position: absolute;
            top: 50%;
            left: 50%;
            font-size: 40pt;
            transform: translate(-50%, -50%);
            color: blue;
        }
    </style>
    <%--<div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>--%>
    
    <div class="image-banner">
        <img
            src="https://previews.123rf.com/images/rawpixel/rawpixel1706/rawpixel170626354/80278133-diverse-group-of-people-smiling-and-arms-raised.jpg"
            alt="People Smiling"
            class="img-fluid"
        />
        <h1>Welcome To Totally Not GoFundMe!</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>What is NotGoFundMe?</h2>
            <p>
                NotGoFundMe is the #1 Most Popular pledge donation site in the world! We have raised more than $500,000 in donations that went to
                charitable causes. We have users from the United States, Europe, Asia, and even Antarctica!
            </p>
        </div>
        <div class="col-md-4">
            <h2>Causes We Support</h2>
            <p>
                We support marathons, record-breaking events, game marathons, blood-drives, pack-a-thon, etc.
                We have supported 500+ events.
            </p>
        </div>
        <div class="col-md-4">
            <h2>The Process</h2>
            <p>
                Raising money on NotGoFundMe is extremely easy.
            </p>
            <ol>
                <li>Create an account</li>
                <li>Create a new event. Make sure to have an eye-catching image and description.</li>
                <li>Share the word</li>
                <li>Mark your event as in-progress to tell your backers, "the show is on!"</li>
                <li>When finished, mark your event as finished.</li>
                <li>Let the money roll in!</li>
            </ol>
        </div>
    </div>

</asp:Content>
