import React from "react";
import { Link } from "react-router-dom"; // Import Link for routing
import { Navbar, MobileNav, Typography } from "@material-tailwind/react";


export function NavbarDefault() {
  const [openNav, setOpenNav] = React.useState(false);

  React.useEffect(() => {
    window.addEventListener(
      "resize",
      () => window.innerWidth >= 960 && setOpenNav(false),
    );
  }, []);

  const navList = (
    <ul className="mt-2 mb-4 flex flex-col gap-2 lg:mb-0 lg:mt-0 lg:flex-row lg:items-center lg:gap-6">
      <Typography
        as="li"
        variant="small"
        color="blue-gray"
        className="flex items-center gap-x-2 p-1 font-medium"
      >
        <Link to="/" className="flex items-center">
          Ushqii
        </Link>
      </Typography>

      {/* Add more links to other pages */}
      <Typography
        as="li"
        variant="small"
        color="blue-gray"
        className="flex items-center gap-x-2 p-1 font-medium"
      >
        <Link to="/another-page" className="flex items-center">
          Another Page
        </Link>
      </Typography>
    </ul>
  );

  return (
    <Navbar className="sticky top-0 z-10 py-4 px-8">
      <div className="relative flex items-center justify-between text-blue-gray-900">
        <Link to="/" className="text-xl font-bold">Your Logo</Link>
        <MobileNav open={openNav} onClose={() => setOpenNav(false)}>
          {navList}
        </MobileNav>
        <div className="hidden lg:block">{navList}</div>
      </div>
    </Navbar>
  );
}
