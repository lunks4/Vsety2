"use client"
import Link from "next/link";
import "./Register.css";
import {Input} from "@nextui-org/react";
import React from 'react';

function Home(){
    const [email, setEmail] = React.useState('');
    const [mailError, setMailError] = React.useState('');
    const [passwordError, setPasswordError] = React.useState('');
    const [confirmPasswordError, setConfirmError] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [confirmPassword, setConfirmPassword] = React.useState('');

    function validateEmail (email: string) {
        const re = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        return re.test(email);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setMailError('');
        setPasswordError('');
        setConfirmError('');

        if (!email) {
            setMailError('Введите почту');
            console.log("papa");
            return;
        }
        console.log(validateEmail(email));
        if (!validateEmail(email)) {
            console.log("mama");
            setMailError('Неверная почта');
            console.log(validateEmail(email));
            return; 
        }
        console.log("papa");
        if (!password) {
            setPasswordError('Введите пароль');
            return;
        }
        if (!confirmPassword) {
            setConfirmError('Введите пароль');
            return;
        }
       
    
        // Простая валидация пароля
        if (password !== confirmPassword) {
          setConfirmError('Пароли не совпадают');
          return;
        }

        try {
            const response = await fetch('https://localhost:7233/api/AccountApi/register', {
              method: 'POST',
              headers: {
                'Content-Type': 'application/json'
              },
              body: JSON.stringify({
                email,
                password,
                confirmPassword
              })
            });
      
            if (!response.ok) {
              throw new Error('Ошибка при регистрации');
            }
      
            // Предполагается, что сервер возвращает успешный ответ
            const data = await response.json();
            console.log('Успешная регистрация:', data);
      
            // Перенаправление на страницу входа после успешной регистрации
            window.location.href = '/Login';
          } catch (err) {
            console.error('Ошибка:', err);
            setMailError('Не удалось зарегистрироваться. Попробуйте снова.');
          }
        
    };
    return ( 
    <div className="body1">
        <div className="registration">
            <div className="auth-box1">
                <div>
                    <h1 className="logo">ВСЕТИ</h1>
                </div>
            </div>
            <div className="flex justify-center my-20">
                <form method="post" className="w-full m-3" onSubmit={handleSubmit}>
                    <div className="flex justify-center text-white text-4xl mb-4 font-bold">
                        <h1>РЕГИСТРАЦИЯ</h1>
                    </div>
                    <div className="mb-3">
                    
                    <Input
                        
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        label="Почта"
                        className="max-h-10"
                    />
                    {mailError && <p style={{ color: 'red' }}>{mailError}</p>}
                    </div>
                    <div className="mb-3">
                    
                    <Input
                        
                        type="password"
                        label="Пароль"
                        value = {password}
                        onChange={(e) => setPassword(e.target.value)}
                        className="max-h-10"
                    />
                    {passwordError && <p style={{ color: 'red' }}>{passwordError}</p>}
                    </div>
                    <div className="mb-3">
                    
                    <Input
                        
                        type="password"
                        label="Повторите пароль"
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        className="max-h-10"
                    />
                    {confirmPasswordError && <p style={{ color: 'red' }}>{confirmPasswordError}</p>}
                    </div>
                    <button type="submit" className="btn btn-primary">
                    Зарегистрироваться
                    </button>
                    <div className="flex justify-center text-white font-semibold text-xl mt-5">
                    УЖЕ ЕСТЬ АККАУНТ? <Link href="/Login" className="link">ВОЙТИ</Link>
                    </div>
                </form>
            </div>
        </div>
    </div>)
}

export default Home;