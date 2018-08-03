
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelManagement.Models;
namespace HotelManagement.Controllers
{
    public class HomeController : ApiController
    {
        static int HotelIdIndex=104; 
       static  List<HotelDetails> Hotels = new List<HotelDetails>()
        {
            new HotelDetails{ HotelId="H101",HotelName="The Residence",AvailableRooms=10,HotelAddress="Viman Nagar"},
            new HotelDetails{ HotelId="H102",HotelName="Le Meridian",AvailableRooms=20,HotelAddress="Kharadi"},
            new HotelDetails{ HotelId="H103",HotelName="Hyatt",AvailableRooms=20,HotelAddress="Viman Nagar"}

        };
        
        [HttpGet]
        
        public Response DisplayHotelDetails()
        {
            try
            {
                return new Response()
                {
                    Details = Hotels,
                    status = new Status()
                    {
                        ApiStatus = status.Success,
                        ErrorMessage ="ALL Elements Displayed Successfully!!",
                        StatusCode = 200
                    }
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Details = null,
                    status = new Status()
                    {
                        ApiStatus = status.Failure,
                        StatusCode = 500,
                        ErrorMessage = "Internal Server Error"
                    }
                };
            }
        }



        [HttpGet]
        public Response SearchHotelById(String id)
        {
            List<HotelDetails> searchObject = new List<HotelDetails>();
            try {
                searchObject .Add(Hotels.Find(Extract => Extract.HotelId.Equals(id)));
                var search = Hotels.Find(Extract => Extract.HotelId.Equals(id));
                if (search==null)
                    throw new Exception("Element Not Found");
                else
                    return new Response()
                    {
                        Details = searchObject,
                        status = new Status()
                        {
                            ApiStatus = status.Success,
                            ErrorMessage = "Hotel Details Fetched Successfully Using ID",
                            StatusCode = 200
                        }
                    };
            }
            catch(Exception ex)
            {
                return new Response()
                {
                    Details = null,
                    status = new Status()
                    {
                        ApiStatus = status.Failure,
                        StatusCode = 404,
                        ErrorMessage = ex.Message
                    }
                };
            }
            

}

        [HttpPost]
        public Response CreateNewHotel(HotelDetails searchObject)
        {
            try
            {
                    HotelIdIndex += 1;
                    searchObject.HotelId = "H" + "" + HotelIdIndex;
                    Hotels.Add(searchObject);
                    return new Response()
                    {
                        status = new Status()
                        {
                            ApiStatus = status.Success,
                            ErrorMessage = "Hotel Added Successfully",
                            StatusCode = 201
                        }
                    };
                
            }

            catch (Exception ex)
            {
                return new Response()
                {
                    Details = null,
                    status = new Status()
                    {
                        ApiStatus = status.Failure,
                        StatusCode = 500,
                        ErrorMessage = "Internal Server Error"
                    }
                };
            }
        }



        [HttpPut]
        public Response BookingDetails(String id,int NoOfRooms)
        {
            
            try
            {
                 var searchObject=Hotels.Find(Extract => Extract.HotelId.Equals(id));
                if (searchObject == null)
                    throw new Exception("Element Not Found");
                else
                {
                    if(searchObject.AvailableRooms<NoOfRooms)
                    return new Response()
                    {
                        
                        status = new Status()
                        {
                            ApiStatus = status.Success,
                            ErrorMessage = "Hotel Does not contain Enough Rooms",
                            StatusCode = 200

                        }
                    };
                    else
                    {
                        return new Response()
                        {
                            
                            status = new Status()
                            {
                                ApiStatus = status.Failure,
                                ErrorMessage = "Rooms Booked Successfully",
                                StatusCode = 200

                            }
                        };
                    }
                }
            }
              catch(Exception ex)
            {
                 return new Response()
    {
                   Details = null,
                    status = new Status()
                    {
                        ApiStatus = status.Failure,
                        StatusCode = 404,
                        ErrorMessage = ex.Message
                    }
                };
}

        }

        
[HttpDelete]
        public Response DeleteHotel(String id)
        {
            HotelDetails searchObject = null;
            try
            {
                searchObject = Hotels.Find(Extract => Extract.HotelId.Equals(id));
                if (searchObject == null)
                    throw new Exception("Element Not Found");
                else
                {
                    Hotels.Remove(searchObject);
                    return new Response()
                    {

                        status = new Status()
                        {
                            ApiStatus = status.Failure,
                            ErrorMessage = "Hotel Removed Successfully",
                            StatusCode = 200

                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Details = null,
                    status = new Status()
                    {
                        ApiStatus = status.Failure,
                        StatusCode = 404,
                        ErrorMessage = ex.Message
                    }
                };
            }
        }
        
    }
}
