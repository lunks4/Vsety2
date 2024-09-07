"use client"
import React, {useState} from "react";
import "./MySelf.css";
import {Navbar, NavbarBrand, NavbarContent, NavbarItem, Link, Input, DropdownItem, DropdownTrigger, Dropdown, DropdownMenu, Avatar, AvatarIcon, Button, Image, User, DateValue} from "@nextui-org/react";
import {Card, CardHeader, CardBody, CardFooter,} from "@nextui-org/react";
import {Select, SelectSection, SelectItem} from "@nextui-org/select";
import {DatePicker} from "@nextui-org/react";
import {AcmeLogo} from "../AcmeLogo.jsx";
import {SearchIcon} from "../SearchIcon.jsx";
import {genders} from "./gender";

function AboutMySelf(){
    const [file, setFile] = useState('/default-avatar.png');
    const [fileServer, setFileServer] = useState(null);
    const [fileName, setFileName] = useState(''); 
    const [Name, setName] = React.useState('');
    const [Surname, setSurname] = React.useState('');
    const [Gender, setGender] = React.useState('');
    const [City, setCity] = React.useState('');
    const [Birthday, setBirthday] = React.useState<DateValue|null>();
    const [Nickname, setNickname] = React.useState('');
    const [Description, setDescription] = React.useState('');
    const [errorName, setErrorName] = React.useState('');
    const [errorSurname, setErrorSurname] = React.useState('');
    const [errorGender, setErrorGender] = React.useState('');
    const [errorCity, setErrorCity] = React.useState('');
    const [errorBirthday, setErrorBirthday] = React.useState('');
    const [errorNickname, setErrorNickname] = React.useState('');

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

    function getCookie(name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
      }

    const handleSubmit = async (event) => {
        event.preventDefault();
        setErrorName('');
        setErrorSurname('');
        setErrorGender('');
        setErrorCity('');
        setErrorBirthday('');
        setErrorNickname('');

        const formData = new FormData();
        formData.append('avatar', fileServer);

        if (!Name) {
            setErrorName('Введите имя');
            return;
        }
        if (!Surname) {
            setErrorSurname('Введите фамилию');
            return;
        }
        if (!Gender) {
            setErrorGender('Введите пол');
            return;
        }
        if (!City) {
            setErrorCity('Введите населенный пункт');
            return;
        }
        if (!Birthday) {
            setErrorBirthday('Введите дату рождения');
            return;
        }
        if (!Nickname) {
            setErrorNickname('Введите ник');
            return;
        }

        formData.append("Name", Name);
        formData.append("Surname", Surname);
        formData.append("Gender", Gender);
        formData.append("City", City);
        formData.append("Birthday", String(Birthday));
        formData.append("Nickname", Nickname);
        if (Description)
        {formData.append("Description", Description);}


        try {
            const jwtToken = getCookie('authToken');
            const response = await fetch('https://localhost:7233/api/AccountApi/person', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${jwtToken}`,
                  },
                body: formData,
            });

            if (response.ok) {
                console.log('File uploaded successfully!');
                window.location.href = '/Home';
            } else {
                console.log(response.status);
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };

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
                name="Имя Фамилия"
                description="@example"
                avatarProps={{
                icon: <AvatarIcon />
                
                }}
            />
            
        </NavbarContent>
        </Navbar>
        <div className="flex justify-center">
            <div className="flex justify-center mt-2 max-w-[700px] min-h-80 min-w-[700px] shadow-lg rounded-3xl">
                <form onSubmit={handleSubmit}>
                    <div className="flex justify-center mt-3">
                    <Image
                        src={file}
                        alt = "none"
                        radius="full"
                        width={100}
                        height={100}
                        classNames={{
                            img: "object-cover",
                        }}
                    />
                    </div>
                    <div className="flex justify-center mb-2 items-center">
                        <label
                        htmlFor="file-upload"
                        className="cursor-pointer inline-block px-4 py-2 mt-2 rounded-full bg-gradient-to-tr from-sky-700 to-sky-300 text-white shadow-lg">
                        Выбрать файл
                        </label>
                        <input id="file-upload" type="file" onChange={handleFileChange} className="hidden"/>
                        <span className="ml-2">{fileName}</span>
                    </div>
                    
                    
                    <div className="flex flex-col justify-center w-[500px] gap-3 pb-3">
                        <Input label="Имя" size="sm" value={Name} onChange={(e) => setName(e.target.value)}/>
                        {errorName && <p style={{ color: 'red' }}>{errorName}</p>}
                        <Input label="Фамилия" size="sm" value={Surname} onChange={(e) => setSurname(e.target.value)}/>
                        {errorSurname && <p style={{ color: 'red' }}>{errorSurname}</p>}
                        <Select 
                            label="Выберите пол" 
                            value={Gender}
                            onChange={(e) => setGender(e.target.value)}
                        >
                            {genders.map((gender) => (
                            <SelectItem key={gender.key}>
                                {gender.label}
                            </SelectItem>
                            ))}
                            
                        </Select>
                        {errorGender && <p style={{ color: 'red' }}>{errorGender}</p>}
                        <Input label="Населенный пункт" size="sm" value={City} onChange={(e) => setCity(e.target.value)}/>
                        {errorCity && <p style={{ color: 'red' }}>{errorCity}</p>}
                        <DatePicker 
                            label="День рождения" 
                            size="sm"
                            value={Birthday}
                            onChange={(date) => setBirthday(date)}
                            
                        />
                        {errorBirthday && <p style={{ color: 'red' }}>{errorBirthday}</p>}
                        <Input label="Псевдоним" size="sm" value={Nickname} onChange={(e) => setNickname(e.target.value)}/>
                        {errorNickname && <p style={{ color: 'red' }}>{errorNickname}</p>}
                        <Input label="О себе" size="sm" value={Description} onChange={(e) => setDescription(e.target.value)}
                        classNames={{
                            inputWrapper:
                            "h-48",
                            label:
                            "static flex self-start",
                            input:
                            "static flex self-start",
                        }}
                        />
                    </div>
                    <div className="flex justify-center mb-5">
                        <Button type="submit" radius="full" fullWidth className="bg-gradient-to-tr from-blue-900 to-sky-300 text-white shadow-lg">
                            Подтвердить
                        </Button>
                    </div>
                </form>
            </div>
        </div>
        </>)
}

export default AboutMySelf;