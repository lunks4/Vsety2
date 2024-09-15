'use client'
import React from "react";
import {Card, CardHeader, CardBody, CardFooter, Avatar, Button, Image, } from "@nextui-org/react";
import Commentary from "./Commentary";
import "./page.css";

import { PostResponse } from "./page";


type PostProps = {
    post: PostResponse
}

export default function Post({post}: PostProps) {
  const [isFollowed, setIsFollowed] = React.useState(false);
  const [logo, setLogo] = React.useState('');
  const [file, setFile] = React.useState('');
  const [error, setError] = React.useState(false);
  const [person, setPerson] = React.useState('');
  const [isLiked, setIsLiked] = React.useState(false);

  const [effectCompleted, setEffectCompleted] = React.useState(false);

  function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
  }

  const fetchData = async () => {
    const jwtToken = getCookie('authToken');
    const userId = post.userId;
    
      try {
       
        const response = await fetch('https://localhost:7233/homeApi/HomeApi/GetPostLogo', {
          method: 'GET',
          headers: {
              'Authorization': `Bearer ${jwtToken}`,
              'userId': userId,
            },
          });
          
          if (!response.ok) {
              throw new Error('Ошибка при загрузке данных');  // Проверка на успешный запрос
          }

          const avatarBlob = await response.blob();
          const avatar = URL.createObjectURL(avatarBlob);
          setLogo(avatar);  // Устанавливаем загруженные данные

      } catch (error) {
          setError(error.message);  // Устанавливаем ошибку, если запрос не удался
      } finally {
            // Завершаем состояние загрузки
      }

      try {
  
        const response = await fetch('https://localhost:7233/homeApi/HomeApi/GetPersonByUserId', {
          method: 'GET',
          headers: {
              'Authorization': `Bearer ${jwtToken}`,
              'userId': userId,
            }, 
          });
          
          if (!response.ok) {
              throw new Error('Ошибка при загрузке данных');  // Проверка на успешный запрос
          }

          const result = await response.json();  // Преобразование ответа в JSON
          setPerson(result); // Устанавливаем загруженные данные

      } catch (error) {
          setError(error.message);  // Устанавливаем ошибку, если запрос не удался
      } finally {
            // Завершаем состояние загрузки
      }

      const ImgId = post.imgId;
      try {
  
        const response = await fetch('https://localhost:7233/homeApi/HomeApi/GetPostFile', {
          method: 'GET',
          headers: {
              'Authorization': `Bearer ${jwtToken}`,
              'imgId': ImgId,
            },
            
          });
          
          if (!response.ok) {
              throw new Error('Ошибка при загрузке данных');  // Проверка на успешный запрос
          }

          const avatarBlob = await response.blob();
          const avatar = URL.createObjectURL(avatarBlob);
          setFile(avatar);  // Устанавливаем загруженные данные

      } catch (error) {
          setError(error.message);  // Устанавливаем ошибку, если запрос не удался
      } finally {
            // Завершаем состояние загрузки
      }

      

      
  };

    
  
  React.useEffect(() => {
        fetchData();
        setEffectCompleted(true);
  }, []);

  

  const date = new Date(post.time);

  const formatter = new Intl.DateTimeFormat('ru-RU', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    
  });
  
  const formattedDate = formatter.format(date);

  return (
    <div className="flex justify-center my-3">
        <Card className="max-w-[700px] min-h-80">
        <CardHeader className="justify-between">
            <div className="flex">
            <Avatar isBordered radius="full" size="md" className="m-3" src={logo} />
            <div className="flex flex-col gap-1 items-start justify-center">
                <h4 className="text-lg font-semibold leading-none text-default-600">{(person?.name || '') + ' ' + (person?.surname || '')}</h4>
                <h5 className="text-base tracking-tight text-default-400">{formattedDate}</h5>
            </div>
            </div>
        </CardHeader>
        <CardBody className="px-3 overflow-hidden">
            <Image
                width={700}
                alt="NextUI hero Image"
                src={file}
            />
            <div className="flex flex-col gap-1 items-start justify-center max-w-[500px] mt-3">
                <p className="text-2xl text-black">
                {post.description}
                </p>
            </div> 
            
        </CardBody>
        <CardFooter className="gap-3 m-3">
            <div className="flex gap-1">
            <Button isIconOnly color="" aria-label="Like" className="rounded-full" onClick={() => setIsLiked(!isLiked)}>
            <svg xmlns="http://www.w3.org/2000/svg"  color="" width="24" height="24" fill={isLiked?"red":"white"} strokeviewBox="0 0 24 24"  stroke={isLiked?"red":"black"} stroke-width="2" stroke-linecap="round" stroke-linejoin="round" className="lucide lucide-heart"><path d="M19 14c1.49-1.46 3-3.21 3-5.5A5.5 5.5 0 0 0 16.5 3c-1.76 0-3 .5-4.5 2-1.5-1.5-2.74-2-4.5-2A5.5 5.5 0 0 0 2 8.5c0 2.3 1.5 4.05 3 5.5l7 7Z"/></svg>
            </Button> 
            </div>
            <div className="flex gap-1">
            <Button isIconOnly color="" aria-label="Like" className="rounded-full">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" className="lucide lucide-message-square-more"><path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z"/><path d="M8 10h.01"/><path d="M12 10h.01"/><path d="M16 10h.01"/></svg>
            </Button> 
            </div>
            <div className="flex gap-1">
            <Button isIconOnly color="" aria-label="Like" className="rounded-full">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" className="lucide lucide-corner-up-right"><polyline points="15 14 20 9 15 4"/><path d="M4 20v-7a4 4 0 0 1 4-4h12"/></svg>
            </Button> 
            </div>
        </CardFooter>
            
        </Card>
    </div>
  );
}