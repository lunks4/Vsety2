import React from "react";
import {Card, CardHeader, CardBody, CardFooter, Avatar, Button} from "@nextui-org/react";

export default function Commentary() {
  const [isFollowed, setIsFollowed] = React.useState(false);

  return (
    <div className="flex justify-center w-full">
        <Card isHoverable className="w-full shadow-none">
        <CardHeader className="justify-between m-2">
            <div className="flex gap-5">
            <Avatar isBordered radius="full" size="md" src="https://nextui.org/avatars/avatar-1.png" />
            <div className="flex flex-col gap-1 items-start justify-center">
                <h4 className="text-small font-semibold leading-none text-default-600">Zoey Lang</h4>
                <h5 className="text-small tracking-tight text-default-400">11.12.2024</h5>
            </div>
            </div>
        </CardHeader>
        <CardBody className="px-3 py-0 text-small text-default-400 overflow-hidden">
            <p>
            Frontend developer and UI/UX enthusiast. Join me on this coding adventure!
            </p>
        </CardBody>
        <CardFooter className="gap-3 m-2">
            
        </CardFooter>
        </Card>
    </div>
  );
}