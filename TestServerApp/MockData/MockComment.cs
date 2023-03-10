using ServerApp.DTO;
using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServerApp.MockData;

public class MockComment
{
    public List<CommentDTO>GetCommentsDTO()
    {
        return new List<CommentDTO>();
    }
}
