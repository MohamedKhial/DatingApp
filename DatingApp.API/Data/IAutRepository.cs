using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.Models.DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data {
    public interface IAuthRepository {
        Task<User> Login (string username, string password);
        Task<User> 
        Registry (User user, string password);
        Task<bool> UserExists (string name);
    }
    public class AuthRepository : IAuthRepository {
        private readonly DataContext _context;

        public AuthRepository (DataContext context) {
            _context = context;
        }

        public async Task<User> Login (string username, string password) {
            var user = await _context.Users.FirstOrDefaultAsync (x => x.Username == username);
            if (user == null) { return null; }
            if (!VerifyPasswordHash (password, user.PasswordHash, user.PasswordSalt)) { return null; }
            return user;

        }

        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hkhial = new System.Security.Cryptography.HMACSHA256 (passwordSalt)) {

                var ComputeHash = hkhial.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                for (int i = 0; i < ComputeHash.Length; i++) {
                    if (ComputeHash[i] != passwordHash[i]) { return false; }
                }
                return true;
            };
        }

        public async Task<User> Registry (User user, string password) {
            byte[] passwordHash, PasswordSalt;
            // passwordHash = user.PasswordHash;
            // PasswordSalt = user.PasswordSalt;

            CreatePasswordHase (password, out passwordHash, out PasswordSalt);
             user.PasswordHash = passwordHash;
             user.PasswordSalt= PasswordSalt;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync ();

            return user;
        }

        private void CreatePasswordHase (string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hkhial = new System.Security.Cryptography.HMACSHA256 ()) {
                passwordSalt = hkhial.Key;
                passwordHash = hkhial.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
            };
            
        }

        public async Task<bool> UserExists (string name) {
            if (await _context.Users.AnyAsync (x => x.Username == name)){ return true;}
            return false;
        }

    }

}