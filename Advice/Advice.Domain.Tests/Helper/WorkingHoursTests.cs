using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Helper;
using NUnit.Framework;

namespace Advice.Domain.Tests.Helper
{
     [TestFixture]
    public class WorkingHoursTests
    {
         [TestFixtureSetUp]
         public void SetUp()
         { 
         }

         [TearDown]
         public void TearDown()
         {
             SystemTime.Now = () => DateTime.Now;
             SystemTime.Today = () => DateTime.Today;
         }

         private static void SetTestDateTime(int day, int month, int year, int hour = 0, int minute = 0, int second = 0)
         {
             SystemTime.Now = () => new DateTime(year, month, day, hour, minute, second);
             SystemTime.Today = () => new DateTime(year, month, day, 0, 0, 0);
         }

#region *************************** Basic sanity tests for WorkingHours class ***************************
         [Test]
         public void Given_Correct_date_Then_ClosingTime_Set_To_CorrectTime()
         {            
             int day = 28; int month = 1; int year = 2015;
             int hour = 8; int minute = 30; int second = 0;
             SetTestDateTime(day, month, year, hour, minute, second);

             DateTime closingTimeToday = WorkingHours.ClosingTimeToday;

             Assert.AreEqual( 18, closingTimeToday.Hour);
             Assert.AreEqual( 0, closingTimeToday.Minute);

             Assert.AreEqual( day, closingTimeToday.Day );
             Assert.AreEqual( month, closingTimeToday.Month );
             Assert.AreEqual( year, closingTimeToday.Year );

         }

         [Test]
         public void Given_Correct_date_Then_OpeningTime_Set_To_CorrectTime()
         {
             int day = 28; int month = 1; int year = 2015;
             int hour = 8; int minute = 30; int second = 0;
             SetTestDateTime(day, month, year, hour, minute, second);

             DateTime openingTimeToday = WorkingHours.OpeningTimeToday;

             Assert.AreEqual(8, openingTimeToday.Hour);
             Assert.AreEqual(30, openingTimeToday.Minute);
             Assert.AreEqual(day, openingTimeToday.Day);
             Assert.AreEqual(month, openingTimeToday.Month);
             Assert.AreEqual(year, openingTimeToday.Year);

         }
             
         [Test]
         public void Given_Date_MidWeek_Then_TimeClosedOvernight_Set_Correctly()
         {
             int day = 28; int month = 1; int year = 2015;
             SetTestDateTime(day, month, year);
             
             TimeSpan TimeClosedOvernight = WorkingHours.TimeClosedOvernight;

             // shut 18:00 to 08:30
             Assert.AreEqual( 14.5,  TimeClosedOvernight.TotalHours);

         }
#endregion


#region *************************** tests for SLA calculation ***************************

         [Test]
         public void Given_Date_MidWeek_When_SLA_Is_19_Hours_DueDate_Is_ClosingTime_Tomorrow()
         {
             int day = 28; int month = 1; int year = 2015;
             int hour = 8; int minute = 30; int second = 0;
             SetTestDateTime(day, month, year, hour, minute, second);

             int slaTimeInMinutes = 19 * 60; // 08:30 until 18:30 tomorrow with gap overnight
             DateTime? dueDate = WorkingHours.CalculateDueDate(slaTimeInMinutes);

             Assert.IsNotNull( dueDate );

             Assert.AreEqual(day + 1, dueDate.Value.Day);
             Assert.AreEqual(month, dueDate.Value.Month);
             Assert.AreEqual(year, dueDate.Value.Year);

             Assert.AreEqual( WorkingHours.ClosingTime.Hours,   dueDate.Value.Hour);
             Assert.AreEqual( WorkingHours.ClosingTime.Minutes, dueDate.Value.Minute);
         }

         [Test]
         public void Given_Date_On_Friday_When_SLA_Is_19_Hours_Then_DueDate_Should_Be_ClosingTime_On_Following_Monday()
         {
             int day = 23; int month = 1; int year = 2015;
             int hour = 8; int minute = 30; int second = 0;
             SetTestDateTime(day, month, year, hour, minute, second);

             int slaTimeInMinutes = 19 * 60; // 08:30 until 18:30 tomorrow with gap overnight
             DateTime? dueDate = WorkingHours.CalculateDueDate(slaTimeInMinutes);

             Assert.IsNotNull(dueDate);

             Assert.AreEqual( 26, dueDate.Value.Day);
             Assert.AreEqual(month, dueDate.Value.Month);
             Assert.AreEqual(year, dueDate.Value.Year);

             Assert.AreEqual(WorkingHours.ClosingTime.Hours, dueDate.Value.Hour);
             Assert.AreEqual(WorkingHours.ClosingTime.Minutes, dueDate.Value.Minute);
         }

#endregion

    }
}
