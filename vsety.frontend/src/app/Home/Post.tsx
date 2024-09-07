import React from "react";
import {Card, CardHeader, CardBody, CardFooter, Avatar, Button, Image, } from "@nextui-org/react";
import Commentary from "./Commentary";
import { PostResponse } from "./page";

type PostProps = {
    post: PostResponse
}
// {post}: PostProps
export default function Post() {
  const [isFollowed, setIsFollowed] = React.useState(false);
  const a = [1,2,3]
  const b = [...a]

  return (
    <div className="flex justify-center">
        <Card className="max-w-[700px] min-h-80">
        <CardHeader className="justify-between">
            <div className="flex">
            <Avatar isBordered radius="full" size="md" className="m-3" src="https://nextui.org/avatars/avatar-1.png" />
            <div className="flex flex-col gap-1 items-start justify-center">
                <h4 className="text-lg font-semibold leading-none text-default-600">Андрей Пожарский</h4>
                <h5 className="text-base tracking-tight text-default-400">11.12.2024</h5>
            </div>
            </div>
        </CardHeader>
        <CardBody className="px-3 py-0 text-small text-default-400">
            <Image
                width={700}
                alt="NextUI hero Image"
                src="https://nextui-docs-v2.vercel.app/images/hero-card-complete.jpeg"
            />
            <div className="flex flex-col gap-1 items-start justify-center max-w-[500px]">
                <p className="text-lg">
                Frontend developer and UI/UX enthusiast. Join me on this coding adventurewa fefwedfsedfawsefewa fweefwafewafdfweaf!
                </p>
            </div> 
            
        </CardBody>
        <CardFooter className="gap-3 m-3">
            <div className="flex gap-1">
            <Button isIconOnly color="" aria-label="Like" className="rounded-full">
            <svg xmlns="http://www.w3.org/2000/svg"  color="" width="24" height="24" viewBox="0 0 24 24" fill="red" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" className="lucide lucide-heart"><path d="M19 14c1.49-1.46 3-3.21 3-5.5A5.5 5.5 0 0 0 16.5 3c-1.76 0-3 .5-4.5 2-1.5-1.5-2.74-2-4.5-2A5.5 5.5 0 0 0 2 8.5c0 2.3 1.5 4.05 3 5.5l7 7Z"/></svg>
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
        <div className="divide-y">
            <Commentary/>
        </div>
        </Card>
    </div>
  );
}