import type { Metadata } from "next";
import { Saira_Condensed } from "next/font/google";
import "./fonts.css";
import "./globals.css";
import anime from 'animejs/lib/anime.es.js';

const inter = Saira_Condensed({ subsets: ['latin'] , weight: '400'});

export const metadata: Metadata = {
  title: "Vsety",
  description: "",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={inter.className}>{children}</body>
    </html>
  );
}
