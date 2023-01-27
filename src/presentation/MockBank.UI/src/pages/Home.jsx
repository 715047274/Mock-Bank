import React from 'react';

import CustomLink from '@/components/CustomLink';
import {Link} from 'react-router-dom';
export default function Home() {
  return (
    <>
      <main>
        <section className='bg-dark'>
          <div className='flex flex-col items-center justify-center min-h-screen text-white layout'>
              <div className='flex flex-row'>
              <div className="card w-96 bg-base-100 shadow-xl h-96 mx-10">
                  <Link to='/berkeley'>
                  <div className="card-body">
                      <figure className="pt-20">
                          <img src="/img/berkeley/navigation-logo-purple-60.png" alt="berkeley payments"/>
                      </figure>
                  </div>
                  </Link>
              </div>
              <div className="card w-96 bg-base-100 shadow-xl h-96 mx-10">
                  <Link to='/central'>
                  <div className="card-body">
                      <figure className="pt-20">
                          <img src="/img/central/Central Payments Web.webp"alt="central payments"/>
                      </figure>
                  </div>
                  </Link>
              </div>
              </div>
            <footer className='absolute text-blue-500 bottom-2'>
              © {new Date().getFullYear()}{' '}
              <Link to='/PageNotFound'>
                Ceridian Payroll Mock Bank
              </Link>
            </footer>
          </div>
        </section>
      </main>
    </>
  );
}
