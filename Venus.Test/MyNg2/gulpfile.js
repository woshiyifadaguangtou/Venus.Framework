/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
     rimraf = require('rimraf');
gulp.task('clean', function (cb) {
    return rimraf("./wwwroot/angularlib/", cb);
});

gulp.task("copy:lib", function () {
    return gulp.src("node_modules/**/*").pipe(gulp.dest("./wwwroot/angularlib/"));
});