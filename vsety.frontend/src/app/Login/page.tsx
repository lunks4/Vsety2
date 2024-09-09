"use client"
import React, {useState} from "react";
import Link from "next/link";
import "./Autorisation.css";
import {Input} from "@nextui-org/react";
import {EyeFilledIcon} from "./EyeFilledIcon";
import {EyeSlashFilledIcon} from "./EyeSlashFilledIcon";


export default function Home(){
    const [isVisible, setIsVisible] = useState(true);
    const [email, setEmail] = React.useState('');
    const [mailError, setMailError] = React.useState('');
    const [passwordError, setPasswordError] = React.useState('');
    const [password, setPassword] = React.useState('');

    const toggleVisibility = () => setIsVisible(!isVisible);

    function validateEmail (email: string) {
        const re = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        return re.test(email);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setMailError('');
        setPasswordError('');

        if (!email) {
            setMailError('Введите почту');
            return;
        }
        console.log(validateEmail(email));
        if (!validateEmail(email)) {
            setMailError('Неверная почта');
            return; 
        }
        if (!password) {
            setPasswordError('Введите пароль');
            return;
        }

        try {
            const response = await fetch('https://localhost:7233/api/AccountApi/authorize', {
              method: 'POST',
              headers: {
                'Content-Type': 'application/json'
              },
              body: JSON.stringify({
                email,
                password,
              })
            });
      
            if (!response.ok) {
              throw new Error('Ошибка входа');
            }
      
            // Предполагается, что сервер возвращает успешный ответ
            const data = await response.json();
            console.log('Успешный вход:', data);
            const token = data.token;
            document.cookie = `authToken=${token}; path=/; secure; samesite=strict`;
      
            // Перенаправление на страницу входа после успешной регистрации
            window.location.href = '/AboutMySelf';
          } catch (err) {
            console.error('Ошибка:', err);
            setMailError('Не удалось войти. Попробуйте снова.');
          }
        
    };
    return ( <div className="body1">
        <div className="registration">
            <div className="flex justify-center mt-20">
                <form method="post" className="w-full m-3" onSubmit={handleSubmit}>
                    <div className="flex justify-center text-white text-4xl mb-4 font-bold">
                        <h1>ВХОД</h1>
                    </div>
                    <div className="flex justify-center mb-3 mt-5">
                    <span className="text-danger"></span>
                    <Input

                        label="Почта"
                        className="max-h-10"
                        onChange={(e) => setEmail(e.target.value)}
                        value = {email}
                    />
                    </div>
                    {mailError && <p style={{ color: 'red' }}>{mailError}</p>}
                    <div className="flex justify-center mt-5">
                    <span className="text-danger"></span>
                    <Input
                       
                        label="Пароль"
                        className="max-h-10"
                        value = {password}
                        onChange={(e) => setPassword(e.target.value)}
                        endContent={
                            <button className="focus:outline-none" type="button" onClick={toggleVisibility} aria-label="toggle password visibility">
                              {isVisible ? (
                                <EyeSlashFilledIcon className="text-2xl text-default-400 pointer-events-none" />
                              ) : (
                                <EyeFilledIcon className="text-2xl text-default-400 pointer-events-none" />
                              )}
                            </button>
                          }
                          type={isVisible ? "text" : "password"}
                    />
                    {passwordError && <p style={{ color: 'red' }}>{passwordError}</p>}
                    </div>
                    <div className="mt-5">
                    </div>
                    <button type="submit" className="btn btn-primary">
                    ВОЙТИ
                    </button>
                    <div className="mt-20 flex justify-center text-white gap-1 font-semibold">
                   <span>НЕТ АККАУНТА?</span> <Link href="/Registration" className="link"> ЗАРЕГИСТРИРОВАТЬСЯ</Link>
                    </div>
                </form>
            </div>
            <div className="auth-box1">
                <div>
                    <h1 className="logo">ВСЕТИ</h1>
                </div>
            </div>
        </div>
    </div>)
}