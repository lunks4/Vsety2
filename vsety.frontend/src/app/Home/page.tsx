'use client'
import React from "react";
import {Navbar, NavbarBrand, NavbarContent, NavbarItem, Link, Input, DropdownItem, DropdownTrigger, Dropdown, DropdownMenu, Avatar, AvatarIcon, Button, Image, User} from "@nextui-org/react";
import {AcmeLogo} from "../AcmeLogo.jsx";
import {SearchIcon} from "../SearchIcon.jsx";
import  Post from "./Post";


export type PostResponse = {
  id: string 
  time: string
  description: string
  userLikes: string[]
  countLikes: string
  usersComments: string[]
  countComments: string
  userReposts: string[]
  countReposts: string
  imgId: string
  userId: string
}


export default function App() {
    const [data, setData] = React.useState('');
    const [data1, setData1] = React.useState('');
    const [avatar, setAvatar] = React.useState('');// Состояние для хранения данных
    const [loading, setLoading] = React.useState(true);  // Состояние для отображения загрузки
    const [error, setError] = React.useState(null);  // Состояние для ошибки
    const [fileName, setFileName] = React.useState('');
    const [fileServer, setFileServer] = React.useState(null);
    const [file, setFile] = React.useState('/default-avatar.png');
    
    const [Description, setDescription] = React.useState('');

    const [posts, setPosts] = React.useState<PostResponse[]>([]);

    const canvasRef = React.useRef<HTMLCanvasElement>(null);
    const canvasRef1 = React.useRef<HTMLCanvasElement>(null);

    function getCookie(name) {
      const value = `; ${document.cookie}`;
      const parts = value.split(`; ${name}=`);
      if (parts.length === 2) return parts.pop().split(';').shift();
    }

    const handleFileChange = (event) => {
      const file = event.target.files[0];
      if (file) {
          const reader = new FileReader();
          reader.onload = (e) => {
              setFile(e.target.result);
          };
          reader.readAsDataURL(file);
          setFileServer(file);
          setFileName(file.name);
      }
    };

    const handleSubmit = async (e) => {
      e.preventDefault();

      if (!Description && !file) {
          console.log('Нужен либ текст, либо фото')
          return;
      }

      const formData = new FormData();
      formData.append("Description", Description);
      formData.append('file', fileServer);


      try {
        const jwtToken = getCookie('authToken');
        const response = await fetch('https://localhost:7233/homeApi/HomeApi/AddPost', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${jwtToken}`,
              },
            body: formData,
        });

        if (response.ok) {
            console.log('File uploaded successfully!');
            const data = await response.json();
            setPosts([data, ...posts]);
            setDescription('');
            setFile('');
            setFileServer(null);
            setFileName('');
        } else {
            console.log(response.status);
        }
      } catch (error) {
          console.error('Error:', error);
      }
    }


    const fetchData = async () => {
      const jwtToken = getCookie('authToken');
        try {
    
          const response = await fetch('https://localhost:7233/homeApi/HomeApi/GetPerson', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${jwtToken}`,
              },
            });
            
            if (!response.ok) {
                throw new Error('Ошибка при загрузке данных');  // Проверка на успешный запрос
            }

            const result = await response.json();  // Преобразование ответа в JSON
            setData(result);  // Устанавливаем загруженные данные

        } catch (error) {
            setError(error.message);  // Устанавливаем ошибку, если запрос не удался
        } finally {
              // Завершаем состояние загрузки
        }

        try {
          const response1 = await fetch('https://localhost:7233/homeApi/HomeApi/GetAvatar', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${jwtToken}`,
              },
            }); 
          if (!response1.ok) {
            throw new Error('Ошибка при загрузке аватара');  // Проверка на успешный запрос
          }
          const avatarBlob = await response1.blob();
          const avatar = URL.createObjectURL(avatarBlob);
          setAvatar(avatar);
        } catch (error) {
          setError(error.message);  // Устанавливаем ошибку, если запрос не удался
        } finally {
                  // Завершаем состояние загрузки
        }

        try {
  
        const response = await fetch('https://localhost:7233/homeApi/HomeApi/GetAllPosts', {
          method: 'GET',
          headers: {
              'Authorization': `Bearer ${jwtToken}`,
              'page1': '0',
              'count1': '5', 
            },
            
          });
          
          if (!response.ok) {
              throw new Error('Ошибка при загрузке данных');  // Проверка на успешный запрос
          }


          const data1 = await response.json();
          setData1(data1);
          setPosts(data1);
          
        } catch (error) {
          setError(error.message);  // Устанавливаем ошибку, если запрос не удался
        } finally {
          setLoading(false);  // Завершаем состояние загрузки
        }
    };

    React.useEffect(() => {
      fetchData();
    }, []); 

  return (
    <>
    <Navbar isBordered position="static" maxWidth="lg">
         
      <NavbarContent as="div" className="items-center" justify="start">
        
          <AcmeLogo />
  
      </NavbarContent>
      <NavbarContent as="div" className="items-center" justify="center">
        <Input
          classNames={{
            base: "w-64 h-10",
            mainWrapper: "h-full",
            input: "text-small",
            inputWrapper: "h-full font-normal text-default-500 bg-default-400/20 dark:bg-default-500/20",
          }}
          radius="full"
          placeholder="Поиск"
          size="sm"
          startContent={<SearchIcon size={18} />}
          type="search"
        />
        </NavbarContent>

      <NavbarContent as="div" className="items-center" justify="center">
        
          <User   
            name={(data?.name || '') + ' ' + (data?.surname || '')}
            description={'@' + data?.nickname}
            avatarProps={{
              src: avatar
            }}
          />
          
      </NavbarContent>
    </Navbar>
    
      <div className="flex flex-col justify-center items-center">   
        <div className="flex justify-center">
          <form method="post" className="mt-3 w-3/12 max-w-[700px] min-w-[700px]">
            
            <Input
            value={Description}
            onChange={(e) => setDescription(e.target.value)}
            // isClearable
            radius="full"
            classNames={{
              label: "text-black/50 dark:text-white/90",
              input: [
                "font-weight:bold",
                "bg-transparent",
                "text-black/90 dark:text-white/90",
                "placeholder:text-default-700/50 dark:placeholder:text-white/60",
              ],
              innerWrapper: "bg-transparent",
              inputWrapper: [
                
                "h-12",
              ],
            }}
            placeholder="Добавить запись"
            
            startContent={
              <Image
                radius="full"
                isZoomed= {true}
                removeWrapper = {true}
                width={50}
                height={40}
                
                src={avatar}
                alt="none"

                //className="object-cover"
              />
            }
            endContent={
              <>
                
                <label
                htmlFor="file-upload"
                className="cursor-pointer inline-block px-2 py-2 rounded-full bg-gradient-to-tr from-blue-800 to-blue-500 text-white shadow-lg">
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" className="lucide lucide-book-image"><path d="m20 13.7-2.1-2.1a2 2 0 0 0-2.8 0L9.7 17"/><path d="M4 19.5v-15A2.5 2.5 0 0 1 6.5 2H19a1 1 0 0 1 1 1v18a1 1 0 0 1-1 1H6.5a1 1 0 0 1 0-5H20"/><circle cx="10" cy="8" r="2"/></svg>
                </label>
                <input id="file-upload" type="file" onChange={handleFileChange} className="hidden"/>
                <span className="ml-2">{fileName}</span>

                <Button isIconOnly radius="full" size="md" className="bg-gradient-to-tr from-blue-800 to-blue-500 text-white shadow-lg m-1" onClick={handleSubmit}>
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" className="lucide lucide-share"><path d="M4 12v8a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2v-8"/><polyline points="16 6 12 2 8 6"/><line x1="12" x2="12" y1="2" y2="15"/></svg>
                </Button>
              </>
            }
            />
            
          </form>
        </div>

        
        {posts.map(post => (<Post key={post.id} post={post}/>))} 
      </div>
    
    </>
  );
}
