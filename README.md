# ArticlesServerApp
ArticlesServerApp is server-side app for web portal application. It contains following enities:
- Article
- Category
- Comment
- User

## User

Entity User is intended for authentification. There is three types of users:
- Author,
- Reader and
- Administrator
Authentification is implemented with JSON web tokent (JWT).

### Methods
- GetAsync
- GetByIdAsync
- AddAsync
- LoginAsync
- UpdateAsync  
 

|              | User type | Request              | Request data     | Response  |
|--------------|-----------|----------------------|------------------|-----------|
| GetAsync     | ADMIN     | GET /api/User        | /                | UserDTO[] |
| GetByIdAsync | Any user  | GET /api/User/{id}   | /                | UserDTO   |
| AddAsync     | /         | POST /api/User       | UserAddDTO       | /         |
| LoginAsync   | /         | POST /api/User/Login | LoginCredencials | JWToken   |
| UpdateAsync  | Any user  | PUT /api/User/{ID}   | EditUserDTO      | /         | 
 

## Article

Article is main entity in application. It represent one web-portal article.

### Methods
- GetAsync
- GetByIdAsync
- AddAsync
- AddTagsAsync
- UpdateAsync  


|              | User type | Request                                    | Request data                  | Response     |
|--------------|-----------|--------------------------------------------|-------------------------------|--------------|
| GetAsync     | /         | GET /api/Article                           | ArticleFilterDTO (from query) | ArticleDTO[] |
| GetByIdAsync | /         | GET /api/Article/{articleId}               | /                             | ArticleDTO   |
| AddAsync     | Author    | POST /api/Article                          | ArticleAddDTO                 | /            |
| AddTagsAsync | Author    | POST /api/Article/{articleId}/AddTagsAsync | string[]                      | /            |
| UpdateAsync  | Author    | PUT /api/Article/{articleId}               | EditArticleDTO                | /            |

## Category

Main classification between articles is by category. Entity category could have parent category and it results category hierarhy. 

### Methods
- AddCategoryAsync
- GetAsync
- EditCategoryAsync


|                   | User type     | Request                        | Request data   | Response    |
|-------------------|---------------|--------------------------------|----------------|-------------|
| AddCategoryAsync  | Administrator | POST api/Category              | CategoryAddDTO | /           |
| GetAsync          | /             | GET api/Category               | /              | CategoryDTO |
| EditCategoryAsync | Administrator | PUT api/Category/{categoryId}  | CategoryDTO    | /           |

## Comment

Entity comment represent comment in article. It also have parent comment and comment hierarhy. 

### Methods
- AddCommentAsync
- GetAsync
- AddLikeAsync
- RemoveLikeAsync
- AddDislikeAsync
- RemoveDislikeAsync
- ApproveAsync
- RejectAsync
- GetAllAsync


|                    | User type     | Request                                | Request data  | Response        |
|--------------------|---------------|----------------------------------------|---------------|-----------------|
| AddCommentAsync    | Reader        | POST api/Comment                       | CommentAddDTO | /               |
| GetAsync           | Any user      | GET api/Comment                        | /             | CategoryDTO[] |
| AddLikeAsync       | Reader        | PUT api/Comment/Like/{commentId}       | CategoryDTO   | /               |
| RemoveLikeAsync    | Reader        | DELETE api/Comment/Like/{commentId}    | /             | /               |
| AddDislikeAsync    | Reader        | PUT api/Comment/Dislike/{commentId}    | /             | /               |
| RemoveDislikeAsync | Reader        | DELETE api/Comment/Dislike/{commentId} | /             | /               |
| ApproveAsync       | Administrator | PUT api/Comment/Approve/{commentId}    | /             | /               |
| RejectAsync        | Administrator | PUT api/Comment/Reject/{commentId}     | /             | /               |
| GetAllAsync        | Administrator | GET api/Comment/All                    | /             | CategoryDTO[] |

## UserDTO  
|Name|Type|
|----|----|
Id|long
Username|string
FirstName|string
LastName|string
Email|string
Role|UserRole
Articles|Article[]

## UserAddDTO
|Name|Type|Is required|
|----|----|--------|
Username|string|Not required
Password|string|Not required
FirstName|string|Not required
LastName|string|Not required
Email|string|Not required
Role|UserRole|Not required

## LoginCredencials
|Name|Type|Is required|
|----|----|--------|
Username|string|Not required
Password|string|Not required
## JWToken
|Name|Type|
|----|----|
Token|string

## EditUserDTO 
|Name|Type|Is required|
|----|----|--------|
Email|string|Not required
Username|string|Not required
Password|string|Not required

## ArticleDTO
|Name|Type|
|----|----|
Id|int
Title|string
Content|string
Image|byte[]
AuthorName|string
CategoryName|string
Comments|CommentDTO[]
Date|DateTime
Tags|TagDTO[]

## ArticleFilterDTO
|Name|Type|
|----|----|
CategoryId|long
Tag|string
AuthorId|long

## ArticleAddDTO
|Name|Type|
|----|----|
Title|string
Content|string
Image|byte[]
CategoryId|long
Tags|TagDTO[]

## EditArticleDTO
|Name|Type|Is required|
|----|----|--------|
Title|string|Required
Content|string|Required
Image|byte[]|Required
CategoryId|long|Required

## CategoryDTO
|Name|Type|Is required|
|----|----|--------|
Name|string|Not required
Id|long|Not required
ParentId|long|Required

## CategoryAddDTO
|Name|Type|Is required|
|----|----|--------|
Name|string|Not required
ParentId|long|Required

## CommentAddDTO
|Name|Type|Is required|
|----|----|--------|
Text|string|Not required
ArticleId|long|Not required
Parent|long|Required
  
## TagDTO
|Name|Type|
|----|----|
TagName|string
